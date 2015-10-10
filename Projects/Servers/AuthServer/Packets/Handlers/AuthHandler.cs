// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using AuthServer.Attributes;
using AuthServer.Constants.Authentication;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using AuthServer.Network.Packets;
using AuthServer.Network.Sessions;
using AuthServer.Packets.Client.Auth;
using AuthServer.Packets.Server.Auth;
using AuthServer.Packets.Structures.Auth;
using AuthServer.Packets.Structures.Auth.Modules;
using Framework.Cryptography.BNet;
using Framework.Misc;

namespace AuthServer.Packets.Handlers
{
    class AuthHandler
    {
        [AuthMessage(AuthClientMessage.ProofResponse, AuthChannel.Authentication)]
        public static async void HandleProofResponse(ProofResponse proofResponse, AuthSession session)
        {
            for (int i = 0; i < proofResponse.ModuleCount; i++)
            {
                if (proofResponse.State == PasswordModuleState.ClientChallenge)
                {
                    if (session.GameAccount == null && session.GameAccounts?.Count > 1)
                    {
                        session.GameAccount = session.GameAccounts.SingleOrDefault(ga =>
                                                                                   ga.Game + ga.Index == proofResponse.GameAccountGame &&
                                                                                   ga.Region == proofResponse.GameAccountRegion);

                        var proofRequest = new ProofRequest { AccountRegion = session.Account.Region };

                        proofRequest.Modules.Add(new RiskFingerprintModule
                        {
                            AccountRegion = proofRequest.AccountRegion,
                            Data = Manager.ModuleMgr.Modules.SingleOrDefault(m => m.Name == "RiskFingerprint" && m.System == session.Platform)
                        });

                        await session.Send(proofRequest);

                        return;
                    }
                    else
                        session.GameAccount = session.GameAccounts[0];

                    if (session.GameAccount == null)
                        await SendAuthComplete(true, AuthResult.NoGameAccount, session);
                    else if (!session.GameAccount.IsOnline)
                    {
                        await SendAuthComplete(false, AuthResult.GlobalSuccess, session);

                        session.GameAccount.IsOnline = true;
                    }

                }
                else if (proofResponse.State == PasswordModuleState.ClientProof)
                {
                    session.SecureRemotePassword.CalculateU(proofResponse.A);
                    session.SecureRemotePassword.CalculateClientM(proofResponse.A);

                    if (session.SecureRemotePassword.ClientM.Compare(proofResponse.M1))
                    {
                        session.SecureRemotePassword.CalculateServerM(proofResponse.M1);

                        // Assign valid game accounts for the account
                        if (session.Account.GameAccounts != null)
                            session.GameAccounts = session.Account.GameAccounts.Where(ga => ga.Game == session.Program).ToList();

                        await SendProofValidation(session, proofResponse.ClientChallenge);
                    }
                    else
                        await SendAuthComplete(true, AuthResult.BadLoginInformation, session);
                }
            }
        }

        public static async Task SendProofRequest(AuthSession session)
        {
            session.SecureRemotePassword = new SRP6a(session.Account.Salt, session.Account.Email, session.Account.PasswordVerifier);
            session.SecureRemotePassword.CalculateB();

            var proofRequest = new ProofRequest { AccountRegion = session.Account.Region };

            var thumbprintModule = new ThumbprintModule
            {
                AccountRegion = proofRequest.AccountRegion,
                Data = Manager.ModuleMgr.Modules.SingleOrDefault(m => m.Name == "Thumbprint" && m.System == session.Platform),
            };

            thumbprintModule.ModuleData = thumbprintModule.Data.Data.ToByteArray();

            proofRequest.Modules.Add(thumbprintModule);
            proofRequest.Modules.Add(new PasswordModule
            {
                AccountRegion = proofRequest.AccountRegion,
                Data = Manager.ModuleMgr.Modules.SingleOrDefault(m => m.Name == "Password" && m.System == session.Platform),
                SecureRemotePassword = session.SecureRemotePassword,
                State = PasswordModuleState.ServerChallenge
            });

            await session.Send(proofRequest);
        }

        public static async Task SendProofValidation(AuthSession session, byte[] clientChallenge)
        {
            var proofRequest = new ProofRequest { AccountRegion = session.Account.Region };

            proofRequest.Modules.Add(new PasswordModule
            {
                Data = Manager.ModuleMgr.Modules.SingleOrDefault(m => m.Name == "Password" && m.System == session.Platform),
                State = PasswordModuleState.ValidateProof,
                AccountRegion = proofRequest.AccountRegion,
                SecureRemotePassword = session.SecureRemotePassword, 
                Size = 161
            });

            /// SelectGameAccount module
            if (session.GameAccounts?.Count > 1)
            {
                var gameAccountBuffer = new AuthPacket();

                gameAccountBuffer.Write(0, 8);
                gameAccountBuffer.Write(session.GameAccounts.Count, 8);

                session.GameAccounts.ForEach(ga =>
                {
                    gameAccountBuffer.Write(ga.Region, 8);
                    gameAccountBuffer.WriteString(ga.Game + ga.Index, 8, false);
                });

                gameAccountBuffer.Finish();

                proofRequest.Modules.Add(new SelectGameAccountModule
                {
                    Data = Manager.ModuleMgr.Modules.SingleOrDefault(m => m.Name == "SelectGameAccount" && m.System == session.Platform),
                    AccountRegion = proofRequest.AccountRegion,
                    GameAccountData = gameAccountBuffer.Data,
                    Size = (uint)gameAccountBuffer.Data.Length
                });
            }
            else
                proofRequest.Modules.Add(new RiskFingerprintModule
                {
                    Data = Manager.ModuleMgr.Modules.SingleOrDefault(m => m.Name == "RiskFingerprint" && m.System == session.Platform)
                });

            await session.Send(proofRequest);
        }

        public static async Task SendAuthComplete(bool failed, AuthResult result, AuthSession session)
        {
            var authComplete = new Complete { AuthResult = result, Failed = failed };

            if (!failed)
            {
                authComplete.PingTimeout = 0x80005000;

                authComplete.RegulatorInfo = new RegulatorInfo
                {
                    Threshold = 25000000,
                    Rate = 1000
                };

                authComplete.LogonInfo = new LogonInfo
                {
                    Account = session.Account,
                    GameAccount = session.GameAccount
                };
            }

            await session.Send(authComplete);
        }
    }
}

/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.Linq;
using AuthServer.Attributes;
using AuthServer.Constants.Authentication;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using Framework.Cryptography.BNet;
using Framework.Misc;
using Framework.Network.Packets;

namespace AuthServer.Network.Packets.Handlers
{
    class AuthHandler
    {
        public static void SendProofRequest(AuthSession session)
        {
            session.Modules = Manager.Module.Modules.Where(m => m.System == session.Account.OS);

            var thumbprintModule = session.Modules.SingleOrDefault(m => m.Name == "Thumbprint");
            var passwordModule = session.Modules.SingleOrDefault(m => m.Name == "Password");

            var proofRequest = new AuthPacket(AuthServerMessage.ProofRequest);

            // Send two modules (Thumbprint & Password).
            proofRequest.Write(2, 3);

            /// Thumbprint module
            Manager.Module.WriteModuleHeader(session, proofRequest, thumbprintModule);

            // Data 
            proofRequest.Write(thumbprintModule.Data.ToByteArray());

            session.SecureRemotePassword = new SRP6a(session.Account.Salt, session.Account.Email, session.Account.PasswordVerifier);
            session.SecureRemotePassword.CalculateB();

            /// Password module
            Manager.Module.WriteModuleHeader(session, proofRequest, passwordModule);

            // State
            proofRequest.Flush();
            proofRequest.Write(PasswordModuleState.ServerChallenge, 8);

            // Data
            proofRequest.Write(session.SecureRemotePassword.I);
            proofRequest.Write(session.SecureRemotePassword.S);
            proofRequest.Write(session.SecureRemotePassword.B);
            proofRequest.Write(session.SecureRemotePassword.S2);

            session.Send(proofRequest);
        }

        [AuthMessage(AuthClientMessage.ProofResponse, AuthChannel.BattleNet)]
        public static void OnProofResponse(AuthPacket packet, AuthSession session)
        {
            var moduleCount = packet.Read<byte>(3);

            for (int i = 0; i < moduleCount; i++)
            {
                var dataSize = packet.Read<int>(10);
                var state = packet.Read<PasswordModuleState>(8);

                switch (state)
                {
                    case PasswordModuleState.ClientChallenge:
                        SendAuthComplete(false, AuthResult.GlobalSuccess, session);
                        break;
                    case PasswordModuleState.ClientProof:
                        // Wrong password module data size
                        if (dataSize != 0x121)
                            return;

                        var a = packet.Read(0x80);
                        var m1 = packet.Read(0x20);
                        var clientChallenge = packet.Read(0x80);

                        session.SecureRemotePassword.CalculateU(a);
                        session.SecureRemotePassword.CalculateClientM(a);

                        if (session.SecureRemotePassword.ClientM.Compare(m1))
                        {
                            session.SecureRemotePassword.CalculateServerM(m1);

                            SendProofValidation(session, clientChallenge);
                        }
                        else
                            SendAuthComplete(true, AuthResult.BadLoginInformation, session);

                        break;
                    default:
                        break;
                }
            }
        }

        public static void SendProofValidation(AuthSession session, byte[] clientChallenge)
        {
            var passwordModule = session.Modules.SingleOrDefault(m => m.Name == "Password");
            var riskFingerprintModule = session.Modules.SingleOrDefault(m => m.Name == "RiskFingerprint");

            var proofValidation = new AuthPacket(AuthServerMessage.ProofRequest);

            // Send two modules (Password & RiskFingerprint).
            proofValidation.Write(2, 3);

            /// Password module
            Manager.Module.WriteModuleHeader(session, proofValidation, passwordModule, 161);

            // State
            proofValidation.Flush();
            proofValidation.Write(PasswordModuleState.ValidateProof, 8);

            // Data
            proofValidation.Write(session.SecureRemotePassword.ServerM);
            proofValidation.Write(session.SecureRemotePassword.S2);

            /// RiskFingerprint module
            Manager.Module.WriteModuleHeader(session, proofValidation, riskFingerprintModule);

            session.Send(proofValidation);
        }

        public static void SendAuthComplete(bool failed, AuthResult result, AuthSession session)
        {
            var complete = new AuthPacket(AuthServerMessage.Complete);

            complete.Write(failed, 1);

            if (failed)
            {
                complete.Write(false, 1);       // false - disable optional modules
                complete.Write(1, 2);           // 1 - enable AuthResults
                complete.Write(result, 16);     // AuthResults (Error codes)
                complete.Write(0x80000000, 32); // Unknown
            }
            else
            {
                complete.Write(0, 3);
                complete.Write(0x80005000, 32); // Ping request, ~10 secs

                var hasOptionalData = true;

                complete.Write(hasOptionalData, 1);

                if (hasOptionalData)
                {
                    var hasConnectionInfo = true;

                    complete.Write(hasConnectionInfo, 1);

                    if (hasConnectionInfo)
                    {
                        complete.Write(25000000, 32);
                        complete.Write(1000, 32);
                    }
                }

                complete.Write(false, 1);

                complete.WriteString("", 8, false); // FirstName not implemented
                complete.WriteString("", 8, false); // LastName not implemented

                complete.Write(session.Account.Id, 32);

                complete.Write(0, 8);
                complete.Write(0, 64);
                complete.Write(0, 8);

                complete.WriteString(session.Account.Email, 5, false, -1);

                complete.Write(0, 64);
                complete.Write(0, 32);

                complete.Write(0, 8);
            }

            session.Send(complete);
        }
    }
}

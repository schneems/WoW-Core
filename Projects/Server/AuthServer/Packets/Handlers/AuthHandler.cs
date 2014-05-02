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

using AuthServer.Managers;
using AuthServer.Network;
using Framework.Attributes;
using Framework.Constants.Authentication;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Cryptography.BNet;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;

namespace AuthServer.Packets.Handlers
{
    class AuthHandler
    {
        public static void SendProofRequest(AuthSession session)
        {
            // Default modules are Win64
            session.Modules = Manager.Modules.Module64List;

            // Switch module list for different systems
            if (session.Account.OS == "Win")
                session.Modules = Manager.Modules.ModuleList;
            else if (session.Account.OS == "Mc64")
                session.Modules = Manager.Modules.ModuleMacList;

            var proofRequest = new AuthPacket(AuthServerMessage.ProofRequest);

            proofRequest.Write(2, 3);

            session.Modules.ForEach(module =>
            {
                // Only auth modules are supported here.
                if (module.Type == "auth")
                {
                    var moduleStart = module.Hash.Substring(0, 2);

                    switch (moduleStart)
                    {
                        case "36":
                        case "c3":
                        case "b3":
                            proofRequest.WriteFourCC(module.Type);
                            proofRequest.WriteFourCC("\0\0" + session.Account.Region);
                            proofRequest.Write(module.Hash.ToByteArray());
                            proofRequest.Write(module.Size, 10);

                            proofRequest.Write(module.Data.ToByteArray());

                            break;
                        case "2e":
                        case "85":
                        case "19":
                            session.SecureRemotePassword = new SRP6a(session.Account.Salt, session.Account.Email, session.Account.PasswordVerifier);
                            session.SecureRemotePassword.CalculateB();

                            proofRequest.WriteFourCC(module.Type);
                            proofRequest.WriteFourCC("\0\0" + session.Account.Region);
                            proofRequest.Write(module.Hash.ToByteArray());
                            proofRequest.Write(module.Size, 10);

                            // Flush & write the state id
                            proofRequest.Flush();
                            proofRequest.Write(0, 8);

                            proofRequest.Write(session.SecureRemotePassword.I);
                            proofRequest.Write(session.SecureRemotePassword.S);
                            proofRequest.Write(session.SecureRemotePassword.B);
                            proofRequest.Write(session.SecureRemotePassword.S2);

                            break;
                        default:
                            Log.Message(LogType.Debug, "Module '{0}' not used in this state", moduleStart);
                            break;
                    }
                }
            });

            session.Send(proofRequest);
        }

        [AuthMessage(AuthClientMessage.ProofResponse, AuthChannel.BattleNet)]
        public static void OnProofResponse(AuthPacket packet, AuthSession session)
        {
            var moduleCount = packet.Read<byte>(3);

            for (int i = 0; i < moduleCount; i++)
            {
                var dataSize = packet.Read<int>(10);
                var stateId = packet.Read(1)[0];

                switch (stateId)
                {
                    case 1:
                        SendAuthComplete(false, AuthResult.GlobalSuccess, session);
                        break;
                    case 2:
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

                            SendProofVerification(session, clientChallenge);
                        }
                        else
                            SendAuthComplete(true, AuthResult.BadLoginInformation, session);

                        break;
                    default:
                        break;
                }
            }
        }

        public static void SendProofVerification(AuthSession session, byte[] clientChallenge)
        {
            var proofVerification = new AuthPacket(AuthServerMessage.ProofRequest);

            proofVerification.Write(2, 3);

            session.Modules.ForEach(module =>
            {
                // Only auth modules are supported here.
                if (module.Type == "auth")
                {
                    var moduleStart = module.Hash.Substring(0, 2);

                    switch (moduleStart)
                    {
                        case "2e":
                        case "85":
                        case "19":
                            proofVerification.WriteFourCC(module.Type);
                            proofVerification.WriteFourCC("\0\0" + session.Account.Region);
                            proofVerification.Write(module.Hash.ToByteArray());
                            proofVerification.Write(161, 10);

                            // Flush & write the state id
                            proofVerification.Flush();
                            proofVerification.Write(3, 8);

                            proofVerification.Write(session.SecureRemotePassword.ServerM);
                            proofVerification.Write(session.SecureRemotePassword.S2);

                            break;
                        case "5e":
                        case "8c":
                        case "1a":
                            proofVerification.WriteFourCC(module.Type);
                            proofVerification.WriteFourCC("\0\0" + session.Account.Region);
                            proofVerification.Write(module.Hash.ToByteArray());
                            proofVerification.Write(module.Size, 10);

                            break;
                        default:
                            Log.Message(LogType.Debug, "Module '{0}' not used in this state", moduleStart);
                            break;
                    }
                }
            });

            session.Send(proofVerification);
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

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Authentication;
using AuthServer.Network.Packets;
using Framework.Cryptography.BNet;

namespace AuthServer.Packets.Structures.Auth.Modules
{
    class PasswordModule : AuthModuleBase
    {
        public PasswordModuleState State  { get; set; }
        public SRP6a SecureRemotePassword { get; set; }

        public override void WriteData(AuthPacket packet)
        {
            packet.Flush();
            packet.Write(State, 8);

            if (State == PasswordModuleState.ServerChallenge)
            {
                packet.Write(SecureRemotePassword.I);
                packet.Write(SecureRemotePassword.S);
                packet.Write(SecureRemotePassword.B);
                packet.Write(SecureRemotePassword.S2);
            }
            else if (State == PasswordModuleState.ValidateProof)
            {
                packet.Write(SecureRemotePassword.ServerM);
                packet.Write(SecureRemotePassword.S2);
            }
        }
    }
}

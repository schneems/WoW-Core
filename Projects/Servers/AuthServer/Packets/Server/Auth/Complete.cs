// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Authentication;
using AuthServer.Constants.Net;
using AuthServer.Packets.Structures.Auth;

namespace AuthServer.Packets.Server.Auth
{
    class Complete : ServerPacket
    {
        public bool Failed { get; set; }
        public AuthResult AuthResult { get; set; } = AuthResult.GlobalSuccess;
        public uint PingTimeout { get; set; }
        public RegulatorInfo RegulatorInfo { get; set; }
        public LogonInfo LogonInfo { get; set; }

        public Complete() : base(AuthServerMessage.Complete) { }

        public override void Write()
        {
            Packet.Write(Failed, 1);

            if (Failed)
            {
                Packet.Write(false, 1);       // false - disable optional modules
                Packet.Write(1, 2);           // 1 - enable AuthResults
                Packet.Write(AuthResult, 16); // AuthResults (Error codes)
                Packet.Write(0x80000000, 32); // Unknown
            }
            else
            {
                Packet.Write(0, 3);             // No optional modules.

                Packet.Write(PingTimeout, 32);  // Ping time span.

                Packet.Write(true, 1);          // Write regulator rules.
                Packet.Write(true, 1);          // Write regulator info.

                RegulatorInfo.Write(Packet);

                Packet.Write(false, 1);         // Write logon info.

                LogonInfo.Write(Packet);
            }
        }
    }
}

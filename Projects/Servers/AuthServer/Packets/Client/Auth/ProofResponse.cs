// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Constants.Authentication;
using Framework.Constants.Account;

namespace AuthServer.Packets.Client.Auth
{
    class ProofResponse : ClientPacket
    {
        // Base data.
        public int ModuleCount           { get; private set; }
        public PasswordModuleState State { get; private set; }

        // ClientChallenge
        public Region GameAccountRegion  { get; private set; }
        public string GameAccountGame    { get; private set; }

        // ClientProof
        public byte[] A               { get; private set; }
        public byte[] M1              { get; private set; }
        public byte[] ClientChallenge { get; private set; }

        public override void Read()
        {
            ModuleCount = Packet.Read<int>(3);

            for (var i = 0; i < ModuleCount; i++)
            {
                var dataSize = Packet.Read<int>(10);

                State = Packet.Read<PasswordModuleState>(8);

                if (State == PasswordModuleState.ClientChallenge)
                {
                    GameAccountRegion = Packet.Read<Region>(8);
                    GameAccountGame = Packet.ReadString(Packet.Read<byte>(8));
                }
                else if (State == PasswordModuleState.ClientProof)
                {
                    if (dataSize != 0x121)
                        return;

                    A = Packet.Read(0x80);
                    M1 = Packet.Read(0x20);
                    ClientChallenge = Packet.Read(0x80);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}

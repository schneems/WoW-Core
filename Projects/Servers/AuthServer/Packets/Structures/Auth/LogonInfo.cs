// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Network.Packets;
using Framework.Database.Auth.Entities;

namespace AuthServer.Packets.Structures.Auth
{
    class LogonInfo : IServerStruct
    {
        public Account Account { get; set; }
        public GameAccount GameAccount { get; set; }

        public void Write(AuthPacket packet)
        {
            packet.WriteString(Account.GivenName, 8, false);
            packet.WriteString(Account.Surname, 8, false);

            packet.Write(Account.Id, 32);
            packet.Write((byte)Account.Region, 8);
            packet.Write((ulong)Account.Flags, 64);

            packet.Write((byte)GameAccount.Region, 8);
            packet.WriteString(GameAccount.AccountId + "#" + GameAccount.Index, 5, false, -1);
            packet.Write((ulong)GameAccount.Flags, 64);

            packet.Write(Account.LoginFailures, 32);
            packet.Write(0, 8);
        }
    }
}

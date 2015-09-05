// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Misc;
using Framework.Network.Packets;
using Framework.Objects;
using WorldServer.Constants.Net;

namespace WorldServer.Packets.Server.Misc
{
    class AccountDataTimes : ServerPacket
    {
        public SmartGuid PlayerGuid { get; set; }
        public uint[] AccountTimes { get; } = new uint[8];

        public AccountDataTimes() : base(ServerMessage.AccountDataTimes) { }

        public override void Write()
        {
            Packet.Write(PlayerGuid);
            Packet.Write(Helper.GetUnixTime());

            for (var i = 0; i < AccountTimes.Length; i++)
                Packet.Write(AccountTimes[i]);
        }
    }
}

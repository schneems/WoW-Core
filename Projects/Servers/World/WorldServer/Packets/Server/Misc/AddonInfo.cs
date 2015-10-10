// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using WorldServer.Constants.Net;
using WorldServer.Packets.Structures.Misc;
using Framework.Network.Packets;

namespace WorldServer.Packets.Server.Misc
{
    class AddonInfo : ServerPacket
    {
        public List<AddonInfoData> Addons         { get; set; } = new List<AddonInfoData>();
        public List<BannedAddonInfo> BannedAddons { get; set; } = new List<BannedAddonInfo>();

        public AddonInfo() : base(ServerMessage.AddonInfo) { }

        public override void Write()
        {
            Packet.Write(Addons.Count);
            Packet.Write(BannedAddons.Count);

            Addons.ForEach(a => a.Write(Packet));
            BannedAddons.ForEach(ba => ba.Write(Packet));
        }
    }
}

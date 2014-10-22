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

using System.Collections.Generic;
using CharacterServer.Constants.Net;
using CharacterServer.Packets.Structures.Misc;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Misc
{
    class AddonInfo() : IServerPacket(ServerMessage.AddonInfo)
    {
        public List<AddonInfoData> Addons         { get; set; } = new List<AddonInfoData>();
        public List<BannedAddonInfo> BannedAddons { get; set; } = new List<BannedAddonInfo>();

        public override void Write()
        {
            Packet.Write(Addons.Count);
            Packet.Write(BannedAddons.Count);

            Addons.ForEach(a => a.Write(Packet));
            BannedAddons.ForEach(ba => ba.Write(Packet));
        }
    }
}

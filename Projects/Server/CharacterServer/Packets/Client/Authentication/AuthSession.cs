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

using Framework.Network.Packets;

namespace CharacterServer.Packets.Client.Authentication
{
    public class AuthSession : IClientPacket
    {
        public int LoginServerID             { get; private set; }
        public ushort Build                  { get; private set; }
        public uint RegionID                 { get; private set; }
        public uint SiteID                   { get; private set; }
        public uint RealmID                  { get; private set; }
        public sbyte LoginServerType         { get; private set; }
        public sbyte BuildType               { get; private set; }
        public uint LocalChallenge           { get; private set; }
        public ulong DosResponse             { get; private set; }
        public byte[] Digest                 { get; private set; }
        public string Account                { get; private set; }
        public bool UseIPv6                  { get; private set; }
        public byte[] AddonInfo              { get; private set; }
        public int CompressedAddonInfoSize   { get; private set; }
        public int UncompressedAddonInfoSize { get; private set; }

        public override void Read()
        {
            LoginServerID   = Packet.Read<int>();
            Build           = Packet.Read<ushort>();
            RegionID        = Packet.Read<uint>();
            SiteID          = Packet.Read<uint>();
            RealmID         = Packet.Read<uint>();
            LoginServerType = Packet.Read<sbyte>();
            BuildType       = Packet.Read<sbyte>();
            LocalChallenge  = Packet.Read<uint>();
            DosResponse     = Packet.Read<ulong>();
            Digest          = Packet.ReadBytes(20);
            Account         = Packet.ReadString(11);
            UseIPv6         = Packet.GetBit();

            CompressedAddonInfoSize   = Packet.Read<int>();
            UncompressedAddonInfoSize = Packet.Read<int>();

            AddonInfo = Packet.ReadBytes(CompressedAddonInfoSize - 4);
        }
    }
}

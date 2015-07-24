// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Client.Authentication
{
    public class AuthSession : ClientPacket
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
            Account         = Packet.ReadDynamicString(11);
            UseIPv6         = Packet.GetBit();

            CompressedAddonInfoSize   = Packet.Read<int>();
            UncompressedAddonInfoSize = Packet.Read<int>();

            AddonInfo = Packet.ReadBytes(CompressedAddonInfoSize - 4);
        }
    }
}

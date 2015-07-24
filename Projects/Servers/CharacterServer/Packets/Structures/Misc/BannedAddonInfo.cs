// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Misc
{
    class BannedAddonInfo : IServerStruct
    {
        public int Id            { get; set; }
        public uint LastModified { get; set; }
        public int Flags         { get; set; }
        public uint[] NameMD5    { get; set; } = new uint[4];
        public uint[] VersionMD5 { get; set; } = new uint[4];

        public void Write(Packet packet)
        {
            packet.Write(Id);

            for (var i = 0; i < 4; i++)
            {
                packet.Write(NameMD5[i]);
                packet.Write(VersionMD5[i]);
            }

            packet.Write(LastModified);
            packet.Write(Flags);
        }
    }
}

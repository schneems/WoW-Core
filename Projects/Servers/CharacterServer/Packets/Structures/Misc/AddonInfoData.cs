// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Misc
{
    class AddonInfoData : IServerStruct
    {
        public byte Status       { get; set; } = 2;
        public bool InfoProvided { get; set; }
        public bool KeyProvided  { get; set; }
        public bool UrlProvided  { get; set; }
        public byte KeyVersion   { get; set; } = 1;
        public uint Revision     { get; set; }
        public string Url        { get; set; }
        public byte[] KeyData    { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(Status);
            packet.PutBit(InfoProvided);
            packet.PutBit(KeyProvided);
            packet.PutBit(UrlProvided);

            if (UrlProvided)
            {
                packet.PutBits(Url.Length, 8);
                packet.FlushBits();

                packet.WriteString(Url);
            }

            packet.FlushBits();

            if (InfoProvided)
            {
                packet.Write(KeyVersion);
                packet.Write(Revision);
            }

            if (KeyProvided)
                packet.WriteBytes(KeyData);
        }
    }
}

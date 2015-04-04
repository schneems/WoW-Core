// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Character
{
    public class AvailableCharacterTemplateSet : IServerStruct
    {
        public uint TemplateSetID { get; set; }
        public string Name        { get; set; }
        public string Description { get; set; }
        public List<AvailableCharacterTemplateClass> Classes { get; } = new List<AvailableCharacterTemplateClass>();

        public void Write(Packet packet)
        {
            packet.Write(TemplateSetID);
            packet.Write(Classes.Count);

            Classes.ForEach(c => c.Write(packet));

            packet.PutBits(Name.Length, 7);
            packet.PutBits(Description.Length, 10);
            packet.FlushBits();

            packet.WriteString(Name);
            packet.WriteString(Description);
        }
    }
}

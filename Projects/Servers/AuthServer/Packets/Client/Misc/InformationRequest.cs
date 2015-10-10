// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using AuthServer.Packets.Structures.Misc;

namespace AuthServer.Packets.Client.Misc
{
    class InformationRequest : ClientPacket
    {
        public string Program     { get; private set; }
        public string Platform    { get; private set; }
        public string Locale      { get; private set; }
        public string AccountName { get; private set; }

        public List<Component> Components { get; } = new List<Component>();

        public override void Read()
        {
            Program  = Packet.ReadFourCC();
            Platform = Packet.ReadFourCC();
            Locale   = Packet.ReadFourCC();

            var componentCount = Packet.Read<int>(6);

            for (var i = 0; i < componentCount; i++)
            {
                var component = new Component();

                component.Read(Packet);

                Components.Add(component);
            }

            var hasAccountName = Packet.Read<bool>(1);

            if (hasAccountName)
            {
                var accountNameLength = Packet.Read<int>(9) + 3;

                AccountName = Packet.ReadString(accountNameLength);
            }

            // Unknown
            Packet.Read<ulong>(64);
        }
    }
}

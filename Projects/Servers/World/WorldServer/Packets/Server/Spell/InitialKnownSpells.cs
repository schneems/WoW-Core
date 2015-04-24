// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Database.Character.Entities;
using Framework.Network.Packets;
using WorldServer.Constants.Net;

namespace WorldServer.Packets.Server.Spell
{
    class InitialKnownSpells : ServerPacket
    {
        public bool InitialLogin { get; set; }
        public IList<CharacterSpell> KnownSpells { get; set; }

        public InitialKnownSpells() : base(ServerMessage.InitialKnownSpells) { }

        public override void Write()
        {
            Packet.PutBit(InitialLogin);

            Packet.FlushBits();

            Packet.Write(KnownSpells.Count);

            foreach (var cs in KnownSpells)
                Packet.Write(cs.SpellId);
        }
    }
}

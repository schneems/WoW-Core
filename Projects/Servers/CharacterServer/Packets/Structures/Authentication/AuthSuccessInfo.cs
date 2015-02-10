/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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
using CharacterServer.Packets.Structures.Character;
using CharacterServer.Packets.Structures.Misc;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Structures.Authentication
{
    public class AuthSuccessInfo : IServerStruct
    {
        public uint VirtualRealmAddress                      { get; set; }
        public List<VirtualRealmInfo> VirtualRealms          { get; set; } = new List<VirtualRealmInfo>();
        public uint TimeRemain                               { get; set; }
        public uint TimeOptions                              { get; set; }
        public uint TimeRested                               { get; set; }
        public byte ActiveExpansionLevel                     { get; set; }
        public byte AccountExpansionLevel                    { get; set; }
        public bool IsExpansionTrial                         { get; set; }
        public uint TimeSecondsUntilPCKick                   { get; set; }
        public List<RaceClassAvailability> AvailableRaces    { get; set; } = new List<RaceClassAvailability>();
        public List<RaceClassAvailability> AvailableClasses  { get; set; } = new List<RaceClassAvailability>();
        public List<AvailableCharacterTemplateSet> Templates { get; set; } = new List<AvailableCharacterTemplateSet>();
        public bool ForceCharacterTemplate                   { get; set; }
        public ushort NumPlayersHorde                        { get; set; }
        public ushort NumPlayersAlliance                     { get; set; }
        public bool IsVeteranTrial                           { get; set; }
        public uint CurrencyID                               { get; set; }

        public void Write(Packet packet)
        {
            packet.Write(VirtualRealmAddress);
            packet.Write(VirtualRealms.Count);
            packet.Write(TimeRemain);
            packet.Write(TimeOptions);
            packet.Write(TimeRested);
            packet.Write(ActiveExpansionLevel);
            packet.Write(AccountExpansionLevel);
            packet.Write(TimeSecondsUntilPCKick);
            packet.Write(AvailableRaces.Count);
            packet.Write(AvailableClasses.Count);
            packet.Write(Templates.Count);
            packet.Write(CurrencyID);

            VirtualRealms.ForEach(vr => vr.Write(packet));
            AvailableRaces.ForEach(ar => ar.Write(packet));
            AvailableClasses.ForEach(ac => ac.Write(packet));
            Templates.ForEach(t => t.Write(packet));

            packet.PutBit(IsExpansionTrial);
            packet.PutBit(ForceCharacterTemplate);
            packet.PutBit(NumPlayersHorde);
            packet.PutBit(NumPlayersAlliance);
            packet.PutBit(IsVeteranTrial);
            packet.FlushBits();
        }
    }
}

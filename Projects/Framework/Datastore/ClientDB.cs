// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Framework.Database.Data.Entities;

namespace Framework.Datastore
{
    public class ClientDB
    {
        public static List<CharBaseInfo> CharBaseInfo                             { get; set; }
        public static List<CharStartOutfit> CharStartOutfits                      { get; set; }
        public static List<ChrClass> ChrClasses                                   { get; set; }
        public static List<ChrRace> ChrRaces                                      { get; set; }
        public static List<GameTables> GameTables                                 { get; set; }
        public static Lookup<int, ItemModifiedAppearance> ItemModifiedAppearances { get; set; }
        public static Dictionary<uint, ItemAppearance> ItemAppearances            { get; set; }
        public static Dictionary<uint, Item> Items                                { get; set; }
        public static List<NameGen> NameGens                                      { get; set; }
        public static Dictionary<uint, SkillLine> SkillLines                      { get; set; }
        public static List<SkillLineAbility> SkillLineAbilities                   { get; set; }
        public static List<SkillRaceClassInfo> SkillRaceClassInfo                 { get; set; }
        public static Lookup<uint, SpellLevels> SpellLevels                       { get; set; }

        // GameTables
        public static ClientGameTable<GtOCTLevelExperience> GtOCTLevelExperience { get; set; }
        public static ClientGameTable<GtNpcTotalHp> GtNpcTotalHp                 { get; set; }
        public static ClientGameTable<GtNpcTotalHpExp1> GtNpcTotalHpExp1         { get; set; }
        public static ClientGameTable<GtNpcTotalHpExp2> GtNpcTotalHpExp2         { get; set; }
        public static ClientGameTable<GtNpcTotalHpExp3> GtNpcTotalHpExp3         { get; set; }
        public static ClientGameTable<GtNpcTotalHpExp4> GtNpcTotalHpExp4         { get; set; }
        public static ClientGameTable<GtNpcTotalHpExp5> GtNpcTotalHpExp5         { get; set; }
    }
}

// Copyright (c) Arctium Emulation.
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
        public static List<GtOCTLevelExperience> GtOCTLevelExperience             { get; set; }
        public static Lookup<int, ItemModifiedAppearance> ItemModifiedAppearances { get; set; }
        public static Dictionary<uint, ItemAppearance> ItemAppearances            { get; set; }
        public static Dictionary<uint, Item> Items                                { get; set; }
        public static List<NameGen> NameGens                                      { get; set; }
        public static Dictionary<uint, SkillLine> SkillLines                      { get; set; }
        public static List<SkillLineAbility> SkillLineAbilities                   { get; set; }
        public static List<SkillRaceClassInfo> SkillRaceClassInfo                 { get; set; }
        public static Lookup<uint, SpellLevels> SpellLevels                       { get; set; }
    }
}

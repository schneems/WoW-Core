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
using System.Linq;
using Framework.Database.Data.Entities;

namespace Framework.Datastore
{
    public class ClientDB
    {
        public static List<CharBaseInfo> CharBaseInfo                                  { get; set; }
        public static List<CharStartOutfit> CharStartOutfits                           { get; set; }
        public static List<ChrClass> ChrClasses                                        { get; set; }
        public static List<ChrRace> ChrRaces                                           { get; set; }
        public static List<GtOCTLevelExperience> GtOCTLevelExperience                  { get; set; }
        public static Lookup<int, ItemModifiedAppearance> ItemModifiedAppearances      { get; set; }
        public static Dictionary<uint, ItemAppearance> ItemAppearances                 { get; set; }
        public static Dictionary<uint, Item> Items                                     { get; set; }
        public static List<NameGen> NameGens                                           { get; set; }
        public static Dictionary<uint, SkillLine> SkillLines                           { get; set; }
        public static List<SkillLineAbility> SkillLineAbilities                        { get; set; }
    }
}

/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
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

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class SkillLineAbility : Entity
    {
        public uint Id                       { get; set; }
        public uint SkillLine                { get; set; }
        public uint Spell                    { get; set; }
        public RaceMask RaceMask             { get; set; }
        public ClassMask ClassMask           { get; set; }
        public uint MinSkillLineRank         { get; set; }
        public uint SupercedesSpell          { get; set; }
        public uint AcquireMethod            { get; set; }
        public uint TrivialSkillLineRankHigh { get; set; }
        public uint TrivialSkillLineRankLow  { get; set; }
        public uint NumSkillUps              { get; set; }
        public uint UniqueBit                { get; set; }
        public uint TradeSkillCategoryId     { get; set; }

        public bool CheckRaceClassConditions(RaceMask raceMask, ClassMask classMask)
        {
            return (RaceMask.HasFlag(raceMask) && ClassMask.HasFlag(classMask)) ||
                   (RaceMask.HasFlag(raceMask) && ClassMask == ClassMask.All) ||
                   (RaceMask == RaceMask.All && ClassMask.HasFlag(classMask));
        }
    }
}

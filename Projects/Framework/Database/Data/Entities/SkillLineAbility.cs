// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

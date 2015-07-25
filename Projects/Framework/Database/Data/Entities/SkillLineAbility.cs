// Copyright (c) Multi-Emu.
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
                   (RaceMask.HasFlag(raceMask) && ClassMask == 0) ||
                   (RaceMask == RaceMask.All && ClassMask.HasFlag(classMask)) ||
                   (RaceMask == RaceMask.All && ClassMask == ClassMask.All) || 
                   (RaceMask == 0 && ClassMask.HasFlag(classMask)) ||
                   (RaceMask == 0 && ClassMask == 0);
        }
    }
}

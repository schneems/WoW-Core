// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class SkillRaceClassInfo : Entity
    {
        public uint Id               { get; set; }
        public int SkillId           { get; set; }
        public RaceMask RaceMask     { get; set; }
        public ClassMask ClassMask   { get; set; }
        public int Flags             { get; set; }
        public int Availability      { get; set; }
        public int MinLevel          { get; set; }
        public int SkillTierId       { get; set; }

        public bool CheckRaceClassConditions(RaceMask raceMask, ClassMask classMask)
        {
            return (RaceMask.HasFlag(raceMask) && ClassMask.HasFlag(classMask)) ||
                   (RaceMask.HasFlag(raceMask) && ClassMask == ClassMask.All) ||
                   (RaceMask == RaceMask.All && ClassMask.HasFlag(classMask)) ||
                   (RaceMask == RaceMask.All && ClassMask == ClassMask.All);
        }
    }
}

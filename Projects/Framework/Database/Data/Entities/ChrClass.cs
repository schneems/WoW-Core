// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class ChrClass : Entity
    {
        public Class Id                         { get; set; }
        public uint DisplayPower                { get; set; }
        public uint SpellClassSet               { get; set; }
        public uint Flags                       { get; set; }
        public uint CinematicSequenceId         { get; set; }
        public uint AttackPowerPerStrength      { get; set; }
        public uint AttackPowerPerAgility       { get; set; }
        public uint RangedAttackPowerPerAgility { get; set; }
        public uint DefaultSpec                 { get; set; }
    }
}

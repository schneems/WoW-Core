// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterSkill : Entity
    {
        [PrimaryKey]
        public ulong CharacterGuid { get; set; }
        public uint SkillId        { get; set; }
        public uint SkillLevel     { get; set; }

        [ForeignKey("Guid")]
        public virtual Character Character { get; set; }
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterTemplateSkill : Entity
    {
        [PrimaryKey]
        public int SkillId  { get; set; }
        public byte ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual CharacterTemplateClass CharacterTemplateClass { get; set; }
    }
}

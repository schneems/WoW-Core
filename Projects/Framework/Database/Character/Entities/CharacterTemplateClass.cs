// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterTemplateClass : Entity
    {
        [PrimaryKey]
        public byte ClassId      { get; set; }
        public int SetId         { get; set; }
        public byte FactionGroup { get; set; } // Alliance = 3, Horde = 5

        public virtual CharacterTemplateSet CharacterTemplateSet { get; set; }

        public virtual IList<CharacterTemplateAction> CharacterTemplateActions { get; set; }
        public virtual IList<CharacterTemplateData> CharacterTemplateData      { get; set; }
        public virtual IList<CharacterTemplateItem> CharacterTemplateItems     { get; set; }
        public virtual IList<CharacterTemplateSkill> CharacterTemplateSkills   { get; set; }
        public virtual IList<CharacterTemplateSpell> CharacterTemplateSpells   { get; set; }
    }
}

// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterTemplateItem : Entity
    {
        [PrimaryKey]
        public int ItemId      { get; set; }
        public byte ClassId    { get; set; }
        public bool IsEquipped { get; set; }

        [ForeignKey("ClassId")]
        public virtual CharacterTemplateClass CharacterTemplateClass { get; set; }
    }
}

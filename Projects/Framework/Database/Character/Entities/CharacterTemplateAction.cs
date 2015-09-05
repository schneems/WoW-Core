// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterTemplateAction : Entity
    {
        [PrimaryKey]
        public byte ClassId { get; set; }
        public int Action   { get; set; }
        public byte Slot    { get; set; }

        public virtual CharacterTemplateClass CharacterTemplateClass { get; set; }
    }
}

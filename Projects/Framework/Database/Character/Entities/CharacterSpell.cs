// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterSpell : Entity
    {
        [PrimaryKey]
        public ulong CharacterGuid { get; set; }
        public uint SpellId        { get; set; }

        [ForeignKey("Guid")]
        public virtual Character Character { get; set; }
    }
}

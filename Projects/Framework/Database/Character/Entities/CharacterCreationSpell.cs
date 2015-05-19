// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterCreationSpell : Entity
    {
        [PrimaryKey]
        public Race Race    { get; set; }
        public Class Class  { get; set; }
        public uint SpellId { get; set; }
    }
}

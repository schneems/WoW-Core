// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterCreationAction : Entity
    {
        [PrimaryKey]
        public byte Race    { get; set; }
        public byte Class   { get; set; }
        public int Action   { get; set; }
        public byte Slot    { get; set; }
    }
}

// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    [NoPluralization]
    public class SpellLevels : Entity
    {
        public uint Id          { get; set; }
        public uint SpellId     { get; set; }
        public int DifficultyId { get; set; }
        public int BaseLevel    { get; set; }
        public int MaxLevel     { get; set; }
        public int SpellLevel   { get; set; }
    }
}

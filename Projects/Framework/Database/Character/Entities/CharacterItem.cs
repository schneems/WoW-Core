// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Items;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterItem : Entity
    {
        [PrimaryKey]
        public ulong CharacterGuid { get; set; }
        public uint ItemId         { get; set; }
        public byte Bag            { get; set; }
        public EquipmentSlot Slot  { get; set; }
        public ItemMode Mode       { get; set; }
        public bool Equipped       { get; set; }

        [ForeignKey("Guid")]
        public virtual Character Character { get; set; }
    }
}

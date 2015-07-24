// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class ItemModifiedAppearance : Entity
    {
        public uint Id          { get; set; }
        public int ItemId       { get; set; }
        public int Mode         { get; set; }
        public int AppearanceId { get; set; }
        public int FileDataId   { get; set; } // Icon
        public int Version      { get; set; }
    }
}

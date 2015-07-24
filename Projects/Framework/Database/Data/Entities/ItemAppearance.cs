// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class ItemAppearance : Entity
    {
        public uint Id        { get; set; }
        public int DisplayId  { get; set; }
        public int FileDataId { get; set; } // Icon
    }
}

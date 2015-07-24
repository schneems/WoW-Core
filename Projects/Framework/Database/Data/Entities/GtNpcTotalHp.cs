// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    [NoPluralization]
    public class GtNpcTotalHp : Entity
    {
        public int Index  { get; set; }
        public float Data { get; set; }
    }
}

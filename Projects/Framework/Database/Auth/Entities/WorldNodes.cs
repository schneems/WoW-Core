// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class WorldNode : Entity
    {
        public int MapId      { get; set; }
        public string Address { get; set; }
        public ushort Port    { get; set; }
    }
}
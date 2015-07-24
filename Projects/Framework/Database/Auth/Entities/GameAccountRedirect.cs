// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class GameAccountRedirect : Entity
    {
        [PrimaryKey]
        public ulong Key          { get; set; }
        public uint GameAccountId { get; set; }
    }
}

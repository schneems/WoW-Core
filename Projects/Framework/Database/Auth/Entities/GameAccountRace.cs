// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class GameAccountRace : Entity
    {
        public uint GameAccountId { get; set; }
        public byte Race          { get; set; }
        public byte Expansion     { get; set; }

        public virtual GameAccount GameAccount { get; set; }
    }
}

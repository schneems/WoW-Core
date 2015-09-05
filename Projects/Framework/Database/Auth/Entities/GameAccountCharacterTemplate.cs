// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class GameAccountCharacterTemplate : Entity
    {
        public uint GameAccountId { get; set; }
        public int SetId          { get; set; }

        public virtual GameAccount GameAccount { get; set; }
    }
}

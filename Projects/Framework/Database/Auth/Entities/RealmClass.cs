// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class RealmClass : Entity
    {
        public uint RealmId   { get; set; }
        public byte Class     { get; set; }
        public byte Expansion { get; set; }

        public virtual Realm Realm { get; set; }
    }
}

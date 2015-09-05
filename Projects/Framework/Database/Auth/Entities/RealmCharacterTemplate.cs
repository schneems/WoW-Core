// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class RealmCharacterTemplate : Entity
    {
        public uint RealmId { get; set; }
        public int SetId    { get; set; }

        public virtual Realm Realm { get; set; }
    }
}

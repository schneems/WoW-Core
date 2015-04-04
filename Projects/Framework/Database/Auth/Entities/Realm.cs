// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class Realm : Entity
    {
        [AutoIncrement]
        public uint Id        { get; set; }
        public string Name    { get; set; }
        public string IP      { get; set; }
        public ushort Port    { get; set; }
        public uint Category  { get; set; }
        public byte Type      { get; set; }
        public byte State     { get; set; }
        public byte Flags     { get; set; }

        public virtual IList<RealmRace> RealmRaces { get; set; }
        public virtual IList<RealmClass> RealmClasses { get; set; }
        public virtual IList<RealmCharacterTemplate> RealmCharacterTemplates { get; set; }
    }
}

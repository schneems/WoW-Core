// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using LappaORM;

namespace Framework.Database.Bnet
{
    public class Realm : Entity
    {
        [AutoIncrement]
        public uint Id        { get; set; }
        public string Name    { get; set; }
        public uint Flags     { get; set; }
        public int Type       { get; set; }
        public int Category   { get; set; }
        public int Timezone   { get; set; }
        public int Language   { get; set; }
        public int Population { get; set; }

        public virtual IList<RealmVersion> RealmVersions { get; set; }
    }
}

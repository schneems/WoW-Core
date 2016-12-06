// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Runtime.Serialization;
using Framework.Packets.Json.Misc;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmListServerIPAddresses
    {
        [DataMember(Name = "families")]
        public IList<RealmListServerIPAddress> Families { get; set; }
    }
}

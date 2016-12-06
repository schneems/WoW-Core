// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Packets.Json.Misc
{
    [DataContract]
    public class RealmListServerIPFamily
    {
        [DataMember(Name = "family")]
        public int Id { get; set; }

        [DataMember(Name = "addresses")]
        public IList<RealmListServerIPAddress> Addresses { get; set; }
    }
}

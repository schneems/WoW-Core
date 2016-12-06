// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmListUpdates
    {
        [DataMember(Name = "updates")]
        public IList<RealmListUpdatePart> Updates { get; set; }
    }
}

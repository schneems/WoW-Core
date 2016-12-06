// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmListUpdatePart
    {
        [DataMember(Name = "wowRealmAddress")]
        public uint WowRealmAddress { get; set; }

        [DataMember(Name = "update")]
        public RealmEntry Update { get; set; }

        [DataMember(Name = "deleting")]
        public bool Deleting { get; set; }
    }
}

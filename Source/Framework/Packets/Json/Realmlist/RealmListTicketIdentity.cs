// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmListTicketIdentity
    {
        [DataMember(Name = "gameAccountID")]
        public ulong GameAccountId { get; set; }

        [DataMember(Name = "gameAccountRegion")]
        public byte GameAccountRegion { get; set; }
    }
}

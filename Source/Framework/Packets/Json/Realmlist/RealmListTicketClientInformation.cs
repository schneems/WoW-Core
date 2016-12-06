// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmListTicketClientInformation
    {
        [DataMember(Name = "info")]
        public RealmListTicketInformationEntry Info { get; set; }
    }
}

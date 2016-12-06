// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Framework.Packets.Json.Misc
{
    [DataContract]
    public class RealmListServerIPAddress
    {
        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        [DataMember(Name = "port")]
        public ushort Port { get; set; }
    }
}

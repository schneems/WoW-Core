// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;
using Framework.Packets.Json.Misc;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmListTicketInformationEntry
    {
        [DataMember(Name = "platform")]
        public uint Platform { get; set; }

        [DataMember(Name = "currentTime")]
        public int CurrentTime { get; set; }

        [DataMember(Name = "buildVariant")]
        public string BuildVariant { get; set; }

        [DataMember(Name = "timeZone")]
        public string Timezone { get; set; }

        [DataMember(Name = "versionDataBuild")]
        public uint VersionDataBuild { get; set; }

        [DataMember(Name = "audioLocale")]
        public uint AudioLocale { get; set; }

        [DataMember(Name = "version")]
        public GameVersion ClientVersion { get; set; }

        [DataMember(Name = "secret")]
        public byte[] Secret { get; set; } = new byte[32];

        [DataMember(Name = "type")]
        public uint Type { get; set; }

        [DataMember(Name = "textLocale")]
        public int uTextLocale { get; set; }
    }
}

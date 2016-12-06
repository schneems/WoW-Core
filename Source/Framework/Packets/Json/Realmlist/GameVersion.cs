// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Framework.Packets.Json.Misc
{
    [DataContract]
    public class GameVersion
    {
        [DataMember(Name = "versionMajor")]
        public uint Major { get; set; }

        [DataMember(Name = "versionBuild")]
        public uint Build { get; set; }

        [DataMember(Name = "versionMinor")]
        public uint Minor { get; set; }

        [DataMember(Name = "versionRevision")]
        public uint Revision { get; set; }
    }
}

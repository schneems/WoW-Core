// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;
using Framework.Packets.Json.Misc;

namespace Framework.Packets.Json.Realmlist
{
    [DataContract]
    public class RealmEntry
    {
        [DataMember(Name = "wowRealmAddress")]
        public uint WowRealmAddress { get; set; }

        [DataMember(Name = "cfgTimezonesID")]
        public int CfgTimezonesID { get; set; }

        [DataMember(Name = "populationState")]
        public int PopulationState { get; set; }

        [DataMember(Name = "cfgCategoriesID")]
        public int CfgCategoriesID { get; set; }

        [DataMember(Name = "version")]
        public GameVersion Version { get; set; }

        [DataMember(Name = "cfgRealmsID")]
        public int CfgRealmsID { get; set; }

        [DataMember(Name = "flags")]
        public uint Flags { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "cfgConfigsID")]
        public int CfgConfigsID { get; set; }

        [DataMember(Name = "cfgLanguagesID")]
        public int CfgLanguagesID { get; set; }
    }
}

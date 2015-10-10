// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;
using Framework.Misc;

namespace Framework.Remoting.Objects
{
    [DataContract]
    public class WorldServerInfo : ServerInfoBase
    {
        [DataMember]
        public int[] Maps { get; set; }

        public bool Compare(WorldServerInfo info)
        {
            return base.Compare(info) && Maps.Compare(info.Maps);
        }
    }
}

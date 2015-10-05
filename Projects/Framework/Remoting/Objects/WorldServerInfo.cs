// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace Framework.Remoting.Objects
{
    public class WorldServerInfo : ServerInfoBase
    {
        public int Map { get; set; }

        public bool Compare(WorldServerInfo info)
        {
            return base.Compare(info) && Map == info.Map;
        }
    }
}

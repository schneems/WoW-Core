// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Misc
{
    public class ServerInfo
    {
        public uint Realm     { get; set; }
        public int[] Maps     { get; set; }
        public string Address { get; set; }
        public ushort Port    { get; set; }
    }
}

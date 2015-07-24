// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class Module : Entity
    {
        public string Hash   { get; set; }
        public string Type   { get; set; }
        public string Name   { get; set; }
        public string System { get; set; }
        public uint Size     { get; set; }
        public string Data   { get; set; }
    }
}

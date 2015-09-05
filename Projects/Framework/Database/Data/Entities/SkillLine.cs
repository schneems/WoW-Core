// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class SkillLine : Entity
    {
        public uint ID                { get; set; }
        public uint CategoryID        { get; set; }
        public uint CanLink           { get; set; }
        public uint ParentSkillLineID { get; set; }
        public uint Flags             { get; set; }
    }
}

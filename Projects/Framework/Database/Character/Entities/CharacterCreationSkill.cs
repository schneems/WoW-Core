// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterCreationSkill : Entity
    {
        [PrimaryKey]
        public Race Race       { get; set; }
        public Class Class     { get; set; }
        public uint SkillId    { get; set; }
        public uint SkillLevel { get; set; }
    }
}

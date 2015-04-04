// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterCreationData : Entity
    {
        [PrimaryKey]
        public Race Race           { get; set; }
        public Class Class         { get; set; }
        public uint Zone           { get; set; }
        public uint Map            { get; set; }
        public float X             { get; set; }
        public float Y             { get; set; }
        public float Z             { get; set; }
        public float O             { get; set; }
    }
}

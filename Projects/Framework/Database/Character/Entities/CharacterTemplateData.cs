// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterTemplateData : Entity
    {
        [PrimaryKey]
        public byte ClassId { get; set; }
        public ushort MapId { get; set; }
        public ushort Zone  { get; set; }
        public float X      { get; set; }
        public float Y      { get; set; }
        public float Z      { get; set; }
        public float O      { get; set; }

        [ForeignKey("ClassId")]
        public virtual CharacterTemplateClass CharacterTemplateClass { get; set; }
    }
}

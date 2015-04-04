// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Lappa_ORM;

namespace Framework.Database.Character.Entities
{
    public class CharacterTemplateSet : Entity
    {
        public int Id             { get; set; }
        public string Name        { get; set; }
        public string Description { get; set; }

        [ForeignKey("SetId")]
        public virtual IList<CharacterTemplateClass> CharacterTemplateClasses { get; set; }
    }
}

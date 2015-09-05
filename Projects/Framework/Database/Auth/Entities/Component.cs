// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class Component : Entity
    {
        public string Program  { get; set; }
        public string Platform { get; set; }
        public int Build       { get; set; }
    }
}

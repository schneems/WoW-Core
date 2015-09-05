// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    [NoPluralization]
    public class GameTables : Entity
    {
        public int Index      { get; set; }
        public string Name    { get; set; }
        public int NumRows    { get; set; }
        public int NumColumns { get; set; }
    }
}

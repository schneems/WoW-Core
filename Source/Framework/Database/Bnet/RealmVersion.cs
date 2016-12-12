// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using LappaORM;

namespace Framework.Database.Bnet
{
    public class RealmVersion : Entity
    {
        public uint RealmId { get; set; }
        public int Major    { get; set; }
        public int Minor    { get; set; }
        public int Revision { get; set; }
        public int Build    { get; set; }
    }
}

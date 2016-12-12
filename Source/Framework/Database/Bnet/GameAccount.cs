// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using LappaORM;

namespace Framework.Database.Bnet
{
    public class GameAccount : Entity
    {
        [AutoIncrement]
        public uint Id             { get; set; }
        public uint AccountId      { get; set; }
        public string Game         { get; set; }
        public byte Index          { get; set; }
        public byte Region         { get; set; }
        public byte ExpansionLevel { get; set; }
        public bool Online         { get; set; }
        public string JoinTicket   { get; set; }
    }
}

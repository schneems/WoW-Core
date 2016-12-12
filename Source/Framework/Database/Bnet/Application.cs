// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using LappaORM;

namespace Framework.Database.Bnet
{
    public class Application : Entity
    {
        public string Program  { get; set; }
        public string Platform { get; set; }
        public string Locale   { get; set; }
        public int Version     { get; set; }
    }
}

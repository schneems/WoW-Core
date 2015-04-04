// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.General;
using Lappa_ORM;

namespace Framework.Database.Data.Entities
{
    public class ChrRace : Entity
    {
        public Race Id                  { get; set; }
        public uint Flags               { get; set; }
        public uint FactionId           { get; set; }
        public uint ExplorationSoundId  { get; set; }
        public uint MaleDisplayId       { get; set; }
        public uint FemaleDisplayId     { get; set; }
        public uint BaseLanguage        { get; set; }
        public uint CinematicSequenceId { get; set; }
        public uint Alliance            { get; set; }
    }
}

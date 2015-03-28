/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class CorpseData : DescriptorBase
    {
        public CorpseData() : base(ObjectData.End) { }

        public DescriptorField Owner             => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField PartyGUID         => base[0x4, 0x4, MirrorFlags.All];
        public DescriptorField DisplayID         => base[0x8, 0x1, MirrorFlags.All];
        public DescriptorField Items             => base[0x9, 0x13, MirrorFlags.All];
        public DescriptorField SkinID            => base[0x1C, 0x1, MirrorFlags.All];
        public DescriptorField FacialHairStyleID => base[0x1D, 0x1, MirrorFlags.All];
        public DescriptorField Flags             => base[0x1E, 0x1, MirrorFlags.All];
        public DescriptorField DynamicFlags      => base[0x1F, 0x1, MirrorFlags.ViewerDependet];
        public DescriptorField FactionTemplate   => base[0x20, 0x1, MirrorFlags.All];

        public static new int End                => ObjectData.End + 0x21;
    }
}

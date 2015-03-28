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
    class SceneObjectData : DescriptorBase
    {
        public SceneObjectData() : base(ObjectData.End) { }

        public DescriptorField ScriptPackageID => base[0x0, 0x1, MirrorFlags.All];
        public DescriptorField RndSeedVal      => base[0x1, 0x1, MirrorFlags.All];
        public DescriptorField CreatedBy       => base[0x2, 0x4, MirrorFlags.All];
        public DescriptorField SceneType       => base[0x6, 0x1, MirrorFlags.All];

        public static new int End => ObjectData.End + 0x7;
    }
}

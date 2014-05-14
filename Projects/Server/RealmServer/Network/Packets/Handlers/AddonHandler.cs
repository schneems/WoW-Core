/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
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

using System.IO;
using System.IO.Compression;

namespace RealmServer.Network.Packets.Handlers
{
    class AddonHandler
    {
        public static void LoadAddonInfoData(RealmSession session, byte[] packedData, int packedSize, int unpackedSize)
        {
            var unpackedAddonData = new byte[unpackedSize];

            if (packedSize > 4)
            {
                using (var inflate = new DeflateStream(new MemoryStream(packedData), CompressionMode.Decompress))
                {
                    var decompressed = new MemoryStream();
                    inflate.CopyTo(decompressed);


                    decompressed.Seek(0, SeekOrigin.Begin);

                    for (int i = 0; i < unpackedSize; i++)
                        unpackedAddonData[i] = (byte)decompressed.ReadByte();
                }
            }

            HandleAddonInfo(session, unpackedAddonData);
        }

        public static void HandleAddonInfo(RealmSession session, byte[] addonData)
        {

        }
    }
}

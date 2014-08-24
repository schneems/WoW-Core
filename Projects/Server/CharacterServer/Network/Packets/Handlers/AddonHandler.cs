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
using Framework.Constants.Misc;
using Framework.Logging;

namespace CharacterServer.Network.Packets.Handlers
{
    class AddonHandler
    {
        public static void LoadAddonInfoData(CharacterSession session, byte[] packedData, int packedSize, int unpackedSize)
        {
            // Check ZLib header (normal mode)
            if (packedData[0] == 0x78 && packedData[1] == 0x9C)
            {
                var unpackedAddonData = new byte[unpackedSize];

                if (packedSize > 0)
                {
                    using (var inflate = new DeflateStream(new MemoryStream(packedData, 2, packedSize - 6), CompressionMode.Decompress))
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
            else
            {
                Log.Message(LogType.Error, "Wrong AddonInfo for Client '{0}'.", session.GetClientIP());

                session.Dispose();
            }
        }

        public static void HandleAddonInfo(CharacterSession session, byte[] addonData)
        {

        }
    }
}

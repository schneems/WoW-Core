// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.IO.Compression;

namespace Framework.Cryptography
{
    public class Zlib
    {
        public static byte[] Deflate(byte[] data, CompressionLevel level = CompressionLevel.Fastest)
        {
            byte[] compressedData;

            using (var ms = new MemoryStream())
            {
                using (var ds = new DeflateStream(ms, level))
                {
                    ds.Write(data, 0, data.Length);
                    ds.Flush();
                }

                compressedData = ms.ToArray();
            }

            return compressedData;
        }
    }
}

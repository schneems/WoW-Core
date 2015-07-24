// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Cryptography
{
    public class Sha1 : SHA1Managed
    {
        public byte[] Digest { get; private set; }

        public Sha1() { }

        public void Process(byte[] data, int length)
        {
            TransformBlock(data, 0, data.Length, data, 0);
        }

        public void Process(uint data)
        {
            var bytes = BitConverter.GetBytes(data);

            TransformBlock(bytes, 0, 4, bytes, 0);
        }

        public void Process(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            TransformBlock(bytes, 0, bytes.Length, bytes, 0);
        }

        public void Finish(byte[] data, int length)
        {
            TransformFinalBlock(data, 0, data.Length);

            Digest = base.Hash;
        }
    }
}

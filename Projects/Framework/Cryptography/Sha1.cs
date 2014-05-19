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

using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Cryptography
{
    public class Sha1 : SHA1Managed
    {
        public byte[] Digest { get; private set; }

        public Sha1()
        {
            Initialize();
        }

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

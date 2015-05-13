// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.IO.Compression;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Net;
using Framework.Logging;
using Framework.Misc;
using Framework.Network;
using Framework.Network.Packets;
using Framework.Packets.Client.Net;
using Framework.Packets.Server.Net;

namespace Framework.Packets.Handlers
{
    public class NetHandler
    {
        [GlobalMessage(GlobalClientMessage.Ping, SessionState.Authenticated)]
        public static async void HandlePing(Ping ping, SessionBase session)
        {
            Log.Network($"Got ping with Serial: {ping.Serial}, Latency: {ping.Latency}.");

            await session.Send(new Pong { Serial = ping.Serial });
        }

        public static ServerPacket CompressPacket(Packet packet)
        {
            // We need the opcode as uint.
            var msg = BitConverter.GetBytes((uint)packet.Header.Message);

            packet.Data[0] = msg[0];
            packet.Data[1] = msg[1];
            packet.Data[2] = msg[2];
            packet.Data[3] = msg[3];

            var compression = new Compression
            {
                UncompressedSize = packet.Data.Length,
                UncompressedAdler = Helper.Adler32(packet.Data)
            };

            // Compress.
            using (var ms = new MemoryStream())
            {
                using (var ds = new DeflateStream(ms, CompressionLevel.Fastest))
                {
                    ds.Write(packet.Data, 0, packet.Data.Length);
                    ds.Flush();
                }

                compression.CompressedData = ms.ToArray();
            }

            compression.CompressedData[0] -= 1;

            if ((compression.CompressedData[compression.CompressedData.Length - 1] & 8) == 8)
                compression.CompressedData = compression.CompressedData.Combine(new byte[1]);

            compression.CompressedData = compression.CompressedData.Combine(new byte[] { 0x00, 0x00, 0xFF, 0xFF });
            compression.CompressedAdler = Helper.Adler32(compression.CompressedData);

            return compression;
        }
    }
}

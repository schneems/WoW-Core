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
using System.Net;
using System.Text;
using CharacterServer.Managers;
using CharacterServer.Network;
using CharacterServer.Packets.Server.Net;
using Framework.Attributes;
using Framework.Constants.Misc;
using Framework.Constants.Net;
using Framework.Logging;
using Framework.Misc;
using Framework.Packets.Client.Net;

namespace CharacterServer.Packets.Handlers
{
    class NetHandler
    {
        public static void SendConnectTo(CharacterSession session, string ip, ushort port, byte connection = 0)
        {
            var connectTo = new ConnectTo
            {
                Key    = Manager.Redirect.CreateRedirectKey(session.GameAccount.Id),
                Serial = 0xE,
                Con    = connection
            };

            // Fail
            if (connectTo.Key == 0)
            {
                session.Dispose();

                return;
            }

            var payloadData = new byte[0xFF];
            var ipBytes = IPAddress.Parse(ip).GetAddressBytes();

            // 0 - 15, Address, IPv6 not supported for now
            payloadData[0] = ipBytes[0];
            payloadData[1] = ipBytes[1];
            payloadData[2] = ipBytes[2];
            payloadData[3] = ipBytes[3];

            // 16
            payloadData[16] = 0x01;

            // 17 - 20, adler32, changes with compression seed.
            // Let's use a static one for now
            payloadData[17] = 0x43;
            payloadData[18] = 0xfd;
            payloadData[19] = 0xb8;
            payloadData[20] = 0x22;

            // 21
            payloadData[21] = 0x2A;

            var portBytes = BitConverter.GetBytes(port);

            // 22 - 23, Port
            payloadData[22] = portBytes[0];
            payloadData[23] = portBytes[1];

            var msg = "Blossom opens above\nSpines rising to the air\nArctium grows stronger\n\0\0\0\0";

            // 24 - 94, Haiku
            Array.Copy(Encoding.ASCII.GetBytes(msg), 0, payloadData, 24, 71);

            // 94 - 125, static for now...
            Array.Copy(new byte[] { 0xD6, 0xAC, 0x21, 0xE6, 0xB2, 0x7B, 0x06, 0x3D, 0xA9, 0x9C, 0x09, 0x4B, 0xC7, 0x30, 0x48, 0x34, 0xD4, 0xF0, 0x55, 0x3B, 0x1B, 0x1D, 0xC9, 0x5B, 0xFD, 0x3C, 0xB9, 0x30, 0x9D, 0xF5, 0x40, 0xC0 }, 0, payloadData, 94, 32);

            // 126 - 233, 0 for now
            Array.Copy(new byte[108], 0, payloadData, 126, 108);

            // 234 - 253, ranodm for now
            Array.Copy(new byte[0].GenerateRandomKey(20), 0, payloadData, 234, 20);

            var dataOrder = new byte[payloadData.Length];

            for (var i = 0; i < payloadData.Length; i++)
                dataOrder[i] = payloadData[payloadOrder[i]];

            var encrypted = Manager.Redirect.Crypt.Encrypt(dataOrder);

            Array.Copy(encrypted, connectTo.Where, 0x100);

            session.Send(connectTo);
        }

        [GlobalMessage(GlobalClientMessage.LogDisconnect)]
        public static void HandleLogDisconnect(LogDisconnect logDisconnect, CharacterSession session)
        {
            Log.Message(LogType.Debug, "{0} disconnected (Reason: {1}).", session.GetClientIP(), logDisconnect.Reason);
        }

        static byte[] payloadOrder = { 0x57, 0x7E, 0x23, 0x4D, 0x39, 0x46, 0x1E, 0xCE, 0xA0, 0xA8, 0x67, 0xB6, 0x3A, 0x37, 0x01, 0xB2,
                                       0xFC, 0x3C, 0x98, 0x3B, 0x4F, 0x70, 0x38, 0x7A, 0x36, 0x63, 0xC8, 0xBA, 0x7F, 0xDD, 0x0C, 0xFA,
                                       0xEC, 0xC4, 0x79, 0x5E, 0xA4, 0x1B, 0x95, 0x32, 0xF0, 0xF5, 0x81, 0x65, 0xCF, 0xDC, 0x19, 0x1F,
                                       0xC5, 0x11, 0x12, 0x13, 0x14, 0x33, 0xB8, 0xC9, 0x8A, 0xC3, 0xF2, 0x5F, 0x58, 0x52, 0x75, 0xE7,
                                       0xEF, 0xD8, 0xEA, 0xA3, 0x61, 0xD7, 0x9C, 0x18, 0x08, 0xDA, 0xCB, 0xAA, 0x40, 0x51, 0xE4, 0x22,
                                       0xA7, 0xE3, 0x20, 0x02, 0x53, 0x55, 0xD9, 0xA9, 0x96, 0x93, 0xB1, 0xDE, 0x2C, 0xBB, 0xE9, 0x97,
                                       0x6E, 0x48, 0xD0, 0x0B, 0x71, 0x9E, 0x05, 0x8C, 0x62, 0x2F, 0x73, 0xC1, 0x82, 0xB4, 0xE8, 0x5D,
                                       0xD4, 0x26, 0x6A, 0xA2, 0x34, 0x77, 0x1C, 0x83, 0x09, 0xE5, 0x6D, 0x59, 0x88, 0xBF, 0xB9, 0x90,
                                       0x1D, 0x56, 0x64, 0xDB, 0xC0, 0x30, 0x25, 0x9B, 0x2E, 0xAF, 0x9A, 0x5A, 0x43, 0x60, 0x4C, 0x41,
                                       0x42, 0x04, 0xD6, 0x2B, 0xEB, 0x78, 0x86, 0x35, 0x47, 0xF3, 0x2D, 0xAD, 0x89, 0xA5, 0x49, 0xD1,
                                       0x07, 0xC2, 0x74, 0xF7, 0xEE, 0x24, 0xF9, 0xD2, 0x6C, 0x21, 0x5C, 0xE2, 0xD5, 0xF4, 0xBC, 0x69,
                                       0xAE, 0xD3, 0xDF, 0xF6, 0xC7, 0x15, 0x7B, 0x92, 0xAB, 0xFE, 0xCC, 0xFB, 0x8D, 0xE6, 0xED, 0x54,
                                       0xAC, 0x68, 0x9F, 0x8F, 0x5B, 0xB0, 0x0E, 0x27, 0x7D, 0x91, 0x45, 0x2A, 0x44, 0x8B, 0xB7, 0x16,
                                       0xA1, 0x80, 0x99, 0xC6, 0x4E, 0xE0, 0x1A, 0x66, 0x0F, 0xBD, 0x06, 0x76, 0xF8, 0x3E, 0x31, 0x4B,
                                       0xE1, 0x3D, 0x94, 0x6F, 0xBE, 0x85, 0x4A, 0x28, 0x6B, 0x0D, 0xCA, 0x84, 0x50, 0x3F, 0xF1, 0x17,
                                       0x87, 0xA6, 0xFD, 0x0A, 0x72, 0x03, 0x7C, 0x10, 0xB5, 0x29, 0xCD, 0x00, 0xB3, 0x9D, 0x8E };
    }
}

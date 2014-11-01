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

        static byte[] payloadOrder = { 0x89, 0x5A, 0x24, 0xD8, 0xB1, 0x48, 0x9F, 0x7F, 0x75, 0xD9, 0x6F, 0xC4, 0x3F, 0xEA, 0x07, 0xED,
                                       0xB6, 0x25, 0xA1, 0x4B, 0xA4, 0xF6, 0x08, 0x22, 0x47, 0x35, 0xDC, 0xF0, 0xF9, 0xC6, 0x82, 0xE6,
                                       0xCF, 0x53, 0x55, 0x80, 0x5D, 0x6A, 0x45, 0xB3, 0xAA, 0xD0, 0x63, 0x69, 0x3D, 0x50, 0xF7, 0xE0,
                                       0x38, 0x90, 0x15, 0x99, 0xAE, 0x54, 0x1A, 0x19, 0xEE, 0xBF, 0x91, 0x4D, 0xCE, 0x7C, 0x43, 0xE7,
                                       0xB7, 0x7E, 0xF5, 0xDD, 0x95, 0xF3, 0xCC, 0x0D, 0xA3, 0xE4, 0x85, 0xCD, 0xFA, 0xD7, 0xBA, 0xC2,
                                       0x60, 0x7D, 0xDE, 0x83, 0x6E, 0x58, 0xD5, 0x3B, 0x0A, 0x64, 0xC9, 0xBB, 0x40, 0xE8, 0x78, 0x51,
                                       0xD3, 0xC5, 0x96, 0x23, 0xFB, 0xB8, 0x1E, 0x20, 0x59, 0x34, 0x6C, 0xDA, 0xBD, 0x86, 0xA7, 0x2F,
                                       0xA8, 0x7B, 0x98, 0xA5, 0x11, 0x12, 0x13, 0x14, 0x36, 0x8E, 0xFD, 0x8C, 0x9A, 0x4C, 0xC3, 0x52,
                                       0x3A, 0xD6, 0xC7, 0x42, 0x97, 0xEC, 0x2A, 0x31, 0x77, 0x33, 0xF4, 0xEF, 0x32, 0xAC, 0x21, 0x06,
                                       0xC8, 0x2C, 0x5B, 0x7A, 0x01, 0xA0, 0xEB, 0x1B, 0xB5, 0xFC, 0xA2, 0x04, 0x46, 0x61, 0x0C, 0x5C,
                                       0x30, 0xAF, 0x16, 0x26, 0x6B, 0x3E, 0xB4, 0xB0, 0x1C, 0xBE, 0x17, 0x18, 0xCB, 0xE3, 0x6D, 0x65,
                                       0x28, 0xC0, 0x8D, 0x2B, 0xC1, 0x9B, 0x71, 0xE5, 0xB2, 0x57, 0x02, 0xF1, 0x2D, 0x27, 0x74, 0x41,
                                       0x1D, 0x66, 0x73, 0xAD, 0xAB, 0xDF, 0xE2, 0xF8, 0x5E, 0x10, 0x70, 0x76, 0xB9, 0x81, 0xBC, 0x92,
                                       0xD2, 0xA9, 0x9C, 0x79, 0x84, 0xD4, 0x4A, 0xFE, 0x37, 0x0E, 0x5F, 0xE1, 0x29, 0x4F, 0x4E, 0x93,
                                       0x94, 0x62, 0x72, 0x0F, 0x8B, 0x49, 0x88, 0xF2, 0x68, 0x87, 0x8A, 0x09, 0x44, 0x03, 0x39, 0x0B,
                                       0x2E, 0x05, 0x3C, 0xD1, 0x8F, 0xCA, 0x1F, 0xDB, 0x67, 0x56, 0xE9, 0x00, 0x9E, 0xA6, 0x9D };
    }
}

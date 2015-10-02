// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Text;
using CharacterServer.Managers;
using CharacterServer.Network;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Net;
using Framework.Logging;
using Framework.Misc;
using Framework.Packets.Client.Net;
using Framework.Packets.Server.Net;

namespace CharacterServer.Packets.Handlers
{
    class NetHandler
    {
        public static async void SendConnectTo(CharacterSession session, string ip, ushort port, byte connection = 0)
        {
            var connectTo = new ConnectTo
            {
                Key = Manager.Redirect.CreateRedirectKey(session.GameAccount.Id),
                Serial = 0xE,
                Con = connection
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

            var msg = "Blossom opens above\nSpines rising to the air\nArctium Emu grows stronger\n";

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
                dataOrder[i] = payloadData[ConnectTo.PayloadOrder[i]];

            var encrypted = Manager.Redirect.Crypt.Encrypt(dataOrder);

            Array.Copy(encrypted, connectTo.Where, 0x100);

            await session.Send(connectTo);
        }

        [GlobalMessage(GlobalClientMessage.LogDisconnect, SessionState.All)]
        public static void HandleLogDisconnect(LogDisconnect logDisconnect, CharacterSession session)
        {
            Log.Debug($"{session.GetClientInfo()} disconnected (Reason: {logDisconnect.Reason}).");

            session.Dispose();
        }
    }
}

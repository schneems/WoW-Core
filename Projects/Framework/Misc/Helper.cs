// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net.Sockets;
using System.Text;
using Framework.Logging;

namespace Framework.Misc
{
    public class Helper
    {
        public static bool CheckConnection(string ip, int port)
        {
            var canConnect = false;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Connect(ip, port);

                    canConnect = true;
                }
                catch
                {
                }
            }

            return canConnect;
        }

        public static void PrintHeader(string serverName)
        {
            Log.Init(@"_____________World of Warcraft___________");
            Log.Init(@"                   _   _                 ");
            Log.Init(@"    /\            | | (_)                ");
            Log.Init(@"   /  \   _ __ ___| |_ _ _   _ _ __ ___  ");
            Log.Init(@"  / /\ \ | '__/ __| __| | | | | '_ ` _ \ ");
            Log.Init(@" / ____ \| | | (__| |_| | |_| | | | | | |");
            Log.Init(@"/_/    \_\_|  \___|\__|_|\__,_|_| |_| |_|");
            Log.Init(@"           _                             ");
            Log.Init(@"          |_._ _   | __|_ o _._          ");
            Log.Init(@"          |_| | |_||(_||_ |(_| |         ");
            Log.Init("");

            var sb = new StringBuilder();

            sb.Append("_________________________________________");

            var nameStart = (42 - serverName.Length) / 2;

            sb.Insert(nameStart, serverName);
            sb.Remove(nameStart + serverName.Length, serverName.Length);

            Log.Init(sb.ToString());
            Log.Init($"{"www.arctium-emulation.com",33}");

            Log.Message();
            Log.Normal($"Starting Project WildStar {serverName}...");
        }

        public static uint GetUnixTime()
        {
            var baseDate = new DateTime(1970, 1, 1);
            var currentDate = DateTime.Now;
            var ts = currentDate - baseDate;

            return (uint)ts.TotalSeconds;
        }

        public static uint Adler32(byte[] data)
        {
            var a = 0xD8F1u;
            var b = 0x9827u;

            for (var i = 0; i < data.Length; i++)
            {
                a = (a + data[i]) % 0xFFF1;
                b = (b + a) % 0xFFF1;
            }
            return (b << 16) + a;
        }
    }
}

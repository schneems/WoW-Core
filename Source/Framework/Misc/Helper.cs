// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Framework.Logging;

namespace Framework.Misc
{
    public class Helper
    {
        public static void PrintHeader(string serverName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Log.Message(LogTypes.None, "_____________World of Warcraft___________");
            Log.Message(LogTypes.None, "                   _   _                 ");
            Log.Message(LogTypes.None, @"    /\            | | (_)                ");
            Log.Message(LogTypes.None, @"   /  \   _ __ ___| |_ _ _   _ _ __ ___  ");
            Log.Message(LogTypes.None, @"  / /\ \ | '__/ __| __| | | | | '_ ` _ \ ");
            Log.Message(LogTypes.None, @" / ____ \| | | (__| |_| | |_| | | | | | |");
            Log.Message(LogTypes.None, @"/_/    \_\_|  \___|\__|_|\__,_|_| |_| |_|");
            Log.Message(LogTypes.None, "           _                             ");
            Log.Message(LogTypes.None, "          |_._ _   | __|_ o _._          ");
            Log.Message(LogTypes.None, "          |_| | |_||(_||_ |(_| |         ");
            Log.NewLine();

            var sb = new StringBuilder();

            sb.Append("_________________________________________");

            var nameStart = (42 - serverName.Length) / 2;

            sb.Insert(nameStart, serverName);
            sb.Remove(nameStart + serverName.Length, serverName.Length);

            Log.Message(LogTypes.None, sb.ToString());
            Log.Message(LogTypes.None, $"{"www.arctium-emulation.com",33}");

            Log.NewLine();
            Log.Message(LogTypes.Info, $"Starting Project WoW {serverName}...");
            Log.NewLine();
        }

        public static Dictionary<string, object> ParseArgs(string[] args)
        {
            var ret = new Dictionary<string, object>();

            for (var i = 0; i < args.Length; i += 2)
                ret.Add(args[i].TrimStart().Remove(0, 2), (i + 1) < args.Length ? args[i + 1].TrimStart() : null);

            return ret;
        }
    }
}

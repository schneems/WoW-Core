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

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Network.Remoting;
using RemoteServer.Configuration;

namespace RemoteServer
{
    class RemoteServer
    {
        static void Main(string[] args)
        {
            ReadArguments(args);

            Log.Message(LogType.Init, "_____________World of Warcraft_____________");
            Log.Message(LogType.Init, "    __                                     ");
            Log.Message(LogType.Init, "    / |                     ,              ");
            Log.Message(LogType.Init, "---/__|---)__----__--_/_--------------_--_-");
            Log.Message(LogType.Init, "  /   |  /   ) /   ' /    /   /   /  / /  )");
            Log.Message(LogType.Init, "_/____|_/_____(___ _(_ __/___(___(__/_/__/_");
            Log.Message(LogType.Init, "________________RemoteServer_______________");
            Log.Message();

            var ipcServer = new IpcChannel("\{RemoteConfig.IPCBindIP}:\{RemoteConfig.IPCBindPort}");

            ChannelServices.RegisterChannel(ipcServer, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject), "WorldObject.rem", WellKnownObjectMode.Singleton);

            Log.Message(LogType.Normal, "Successfully registered IPC Server Channel with name '{0}'", ipcServer.ChannelName);

            // Create initial remote object
            var remoteObject = new RemoteObject();

            RemotingServices.Marshal(remoteObject);

            while (true)
                Thread.Sleep(1);
        }

        static void ReadArguments(string[] args)
        {
            for (int i = 1; i < args.Length; i += 2)
            {
                switch (args[i - 1])
                {
                    case "-config":
                        RemoteConfig.Initialize(args[i]);
                        break;
                    default:
                        Log.Message(LogType.Error, "'{0}' isn't a valid argument.", args[i - 1]);
                        break;
                }
            }

            if (!RemoteConfig.IsInitialized)
                RemoteConfig.Initialize("./Configs/RemoteServer.conf");
        }

    }
}

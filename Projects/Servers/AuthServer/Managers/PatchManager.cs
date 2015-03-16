/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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

using System.Collections.Generic;
using System.IO;
using AuthServer.Configuration;
using AuthServer.Network.Sessions;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;

namespace AuthServer.Managers
{
    class PatchManager : Singleton<PatchManager>
    {
        Dictionary<string, AuthPacket> patchPackets;

        PatchManager()
        {
            patchPackets = new Dictionary<string, AuthPacket>();

            PreBuildPatchPackets();
        }

        void PreBuildPatchPackets()
        {
            var games = Directory.GetDirectories(AuthConfig.PatchFileDirectory);
            
            foreach (var dir in games)
            {
                var files = Directory.GetFiles(dir);

                foreach (var f in files)
                {
                    var pkt = new AuthPacket();
                    var key = $"/{new DirectoryInfo(dir).Name}/{Path.GetFileNameWithoutExtension(f)}";
                    var value = File.ReadAllBytes(f);

                    pkt.Write(value);

                    patchPackets.Add(key, pkt);

                    Log.Normal($"Loaded patch file for '{key}'.");
                }
            }

            Log.Message();

            IsInitialized = true;
        }

        public void Send(string key, Client client)
        {
            AuthPacket pkt;

            if (patchPackets.TryGetValue(key, out pkt))
                client.SendPacket(pkt);
            else
                Log.Error($"Patch packet for key '{key}' not found.");

            Log.Message();
        }
    }
}

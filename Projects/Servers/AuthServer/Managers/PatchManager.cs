// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

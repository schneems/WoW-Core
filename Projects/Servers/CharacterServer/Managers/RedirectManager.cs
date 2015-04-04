// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using Framework.Constants.Misc;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace CharacterServer.Managers
{
    class RedirectManager : Singleton<RedirectManager>
    {
        public ConcurrentDictionary<int, WorldServer> WorldServers { get; set; }
        public RsaCrypt Crypt { get; set; }

        RedirectManager()
        {
            WorldServers = new ConcurrentDictionary<int, WorldServer>();

            // Initialize RSA crypt
            Crypt = new RsaCrypt();

            Crypt.InitializeEncryption(RsaStore.D, RsaStore.P, RsaStore.Q, RsaStore.DP, RsaStore.DQ, RsaStore.InverseQ);
            Crypt.InitializeDecryption(RsaStore.Exponent, RsaStore.Modulus);

            LoadAvailableWorldServers();
        }

        void LoadAvailableWorldServers()
        {
            var worldServers = DB.Auth.Select<WorldServer>();

            if (worldServers.Count == 0)
                Log.Error("No WorldServers available.");

            worldServers.ForEach(ws =>
            {
                if (WorldServers.TryAdd(ws.MapId, ws))
                    Log.Normal("Added new WorldServer for Map '{0}' at '{1}:{2}'.", ws.MapId, ws.Address, ws.Port);
            });

            Log.Normal($"Loaded {WorldServers.Count} WorldServers.");
            Log.Message();
        }

        public WorldServer GetWorldServer(int mapId)
        {
            WorldServer worldServer;

            // Try to get available world servers for the specific map or for all maps (-1).
            if (WorldServers.TryGetValue(mapId, out worldServer) || WorldServers.TryGetValue(-1, out worldServer))
                if (Helper.CheckConnection(worldServer.Address, worldServer.Port))
                    return worldServer;

            return null;
        }

        public ulong CreateRedirectKey(uint gameAccountId)
        {
            var key = BitConverter.ToUInt64(new byte[0].GenerateRandomKey(8), 0);

            if (DB.Auth.Add(new GameAccountRedirect { GameAccountId = gameAccountId, Key = key }))
                return key;

            return 0;
        }
    }
}

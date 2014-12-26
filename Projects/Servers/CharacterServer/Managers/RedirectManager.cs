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
                Log.Message(LogType.Error, "No WorldServers available.");

            worldServers.ForEach(ws =>
            {
                if (WorldServers.TryAdd(ws.MapId, ws))
                    Log.Message(LogType.Normal, "Added new WorldServer for Map '\{ws.MapId}' at '\{ws.Address}:\{ws.Port}'.");
            });

            Log.Message(LogType.Normal, "Loaded \{WorldServers.Count} WorldServers.");
            Log.Message();
        }

        public WorldServer GetWorldServer(int mapId)
        {
            WorldServer worldServer;

            // Try to get available world servers for the specific map or for all maps (-1).
            if (WorldServers.TryGetValue(mapId, out worldServer) || WorldServers.TryGetValue(-1, out worldServer))
                if (Helpers.CheckConnection(worldServer.Address, worldServer.Port))
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

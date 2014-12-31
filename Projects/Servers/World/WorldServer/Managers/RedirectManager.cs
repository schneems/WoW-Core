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

using System;
using System.Collections.Concurrent;
using Framework.Constants.Misc;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace WorldServer.Managers
{
    class RedirectManager : Singleton<RedirectManager>
    {
        public ConcurrentDictionary<int, WorldNode> WorldNodes { get; set; }
        public RsaCrypt Crypt { get; set; }

        RedirectManager()
        {
            WorldNodes = new ConcurrentDictionary<int, WorldNode>();

            // Initialize RSA crypt
            Crypt = new RsaCrypt();

            Crypt.InitializeEncryption(RsaStore.D, RsaStore.P, RsaStore.Q, RsaStore.DP, RsaStore.DQ, RsaStore.InverseQ);
            Crypt.InitializeDecryption(RsaStore.Exponent, RsaStore.Modulus);

            LoadAvailableWorldNodes();

        }

        void LoadAvailableWorldNodes()
        {
            var worldNodes = DB.Auth.Select<WorldNode>();

            if (worldNodes.Count == 0)
                Log.Message(LogType.Error, "No WorldNodes available.");

            worldNodes.ForEach(ws =>
            {
                if (WorldNodes.TryAdd(ws.MapId, ws))
                    Log.Message(LogType.Normal, "Added new WorldNode for Map '\{ws.MapId}' at '\{ws.Address}:\{ws.Port}'.");
            });

            Log.Message(LogType.Normal, "Loaded {0} WorldNodes.", WorldNodes.Count);
            Log.Message();
        }

        public WorldNode GetWorldNode(int mapId)
        {
            WorldNode worldNode;

            // Try to get available world servers for the specific map or for all maps (-1).
            if (WorldNodes.TryGetValue(mapId, out worldNode) || WorldNodes.TryGetValue(-1, out worldNode))
                if (Helpers.CheckConnection(worldNode.Address, worldNode.Port))
                    return worldNode;

            return null;
        }

        public ulong CreateRedirectKey(ulong characterGuid)
        {
            var key = BitConverter.ToUInt64(new byte[0].GenerateRandomKey(8), 0);

            if (DB.Auth.Add(new CharacterRedirect { Key = key, CharacterGuid = characterGuid }))
                return key;

            return 0;
        }

        public Tuple<Account, GameAccount> GetAccountInfo(ulong key)
        {
            var redirect = DB.Auth.Single<GameAccountRedirect>(gar => gar.Key == key);

            if (redirect != null)
            {
                var gameAccount = DB.Auth.Single<GameAccount>(ga => ga.Id == redirect.GameAccountId);

                if (gameAccount != null)
                {
                    var account = DB.Auth.Single<Account>(a => a.Id == gameAccount.AccountId);

                    if (account != null)
                        return Tuple.Create(account, gameAccount);
                }
            }

            return null;
        }

        public bool DeleteGameAccountRedirect(ulong key)
        {
            return DB.Auth.Delete<GameAccountRedirect>(gar => gar.Key == key);
        }
    }
}

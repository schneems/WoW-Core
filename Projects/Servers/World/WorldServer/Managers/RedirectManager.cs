// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
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
                Log.Error("No WorldNodes available.");

            worldNodes.ForEach(ws =>
            {
                if (WorldNodes.TryAdd(ws.MapId, ws))
                    Log.Normal("Added new WorldNode for Map '{0}' at '{1}:{2}'.", ws.MapId, ws.Address, ws.Port);
            });

            Log.Normal("Loaded {0} WorldNodes.", WorldNodes.Count);
            Log.Message();
        }

        public WorldNode GetWorldNode(int mapId)
        {
            WorldNode worldNode;

            // Try to get available world servers for the specific map or for all maps (-1).
            if (WorldNodes.TryGetValue(mapId, out worldNode) || WorldNodes.TryGetValue(-1, out worldNode))
                if (Helper.CheckConnection(worldNode.Address, worldNode.Port))
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

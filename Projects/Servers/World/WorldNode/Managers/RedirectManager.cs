// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Misc;
using Framework.Remoting.Objects;
using WorldNode.Network;

namespace WorldNode.Managers
{
    class RedirectManager : Singleton<RedirectManager>
    {
        public RsaCrypt Crypt { get; set; }

        RedirectManager()
        {
            // Initialize RSA crypt
            Crypt = new RsaCrypt();

            Crypt.InitializeEncryption(RsaStore.D, RsaStore.P, RsaStore.Q, RsaStore.DP, RsaStore.DQ, RsaStore.InverseQ);
            Crypt.InitializeDecryption(RsaStore.Exponent, RsaStore.Modulus);
        }

        public WorldServerInfo GetWorldServer(uint realmId)
        {
            var servers = from cs in Server.WorldService.Servers.Values where cs is WorldServerInfo && cs.RealmId == WorldNode.Info.Realm select cs;
            var WorldServer = servers.OrderBy(cs => cs.ActiveConnections).FirstOrDefault() as WorldServerInfo;

            return WorldServer;
        }

        public WorldServerInfo GetWorldServer(int mapId)
        {
            var servers = from ws in Server.WorldService.Servers.Values where (((WorldServerInfo)ws).Maps.Contains(mapId) ||
                          ((WorldServerInfo)ws).Maps.Contains(-1)) && ws.RealmId == WorldNode.Info.Realm select ws;

            var worldServer = servers.OrderBy(ws => ws.ActiveConnections).FirstOrDefault() as WorldServerInfo;

            return worldServer;
        }

        public WorldNodeInfo GetWorldNode(int mapId)
        {
            var servers = from wn in Server.NodeService.Servers.Values where (((WorldNodeInfo)wn).Maps.Contains(mapId) ||
                          ((WorldNodeInfo)wn).Maps.Contains(-1)) && wn.RealmId == WorldNode.Info.Realm select wn;

            var worldnode = servers.OrderBy(wn => wn.ActiveConnections).FirstOrDefault() as WorldNodeInfo;

            return worldnode;
        }

        public ulong CreateRedirectKey(uint gameAccountId, ulong playerGuid)
        {
            var key = BitConverter.ToUInt64(new byte[0].GenerateRandomKey(8), 0);

            Server.NodeService.Update(key, Tuple.Create(gameAccountId, playerGuid));

            return key;
        }

        public Tuple<Account, GameAccount, ulong> GetAccountInfo(ulong key)
        {
            Tuple<uint, ulong> redirectData;

            if (Server.WorldService.Redirects.TryGetValue(key, out redirectData))
            {
                var gameAccount = DB.Auth.Single<GameAccount>(ga => ga.Id == redirectData.Item1);

                if (gameAccount != null)
                {
                    var account = DB.Auth.Single<Account>(a => a.Id == gameAccount.AccountId);

                    if (account != null)
                        return Tuple.Create(account, gameAccount, redirectData.Item2);
                }
            }

            return null;
        }

        public void DeleteRedirectKey(ulong key)
        {
            Server.NodeService.Update(key, null);
        }
    }
}

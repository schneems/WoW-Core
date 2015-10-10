// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Misc;
using Framework.Remoting.Objects;
using WorldServer.Network;

namespace WorldServer.Managers
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

        public WorldNodeInfo GetWorldNode(int mapId)
        {
            var servers = from wn in Server.NodeService.Servers.Values where (((WorldNodeInfo)wn).Maps.Contains(mapId) ||
                          ((WorldNodeInfo)wn).Maps.Contains(-1)) && wn.RealmId == WorldServer.Info.Realm select wn;

            var worldnode = servers.OrderBy(wn => wn.ActiveConnections).FirstOrDefault() as WorldNodeInfo;

            return worldnode;
        }

        public WorldServerInfo GetWorldServer(int mapId)
        {
            var servers = from ws in Server.WorldService.Servers.Values where (((WorldServerInfo)ws).Maps.Contains(mapId) ||
                          ((WorldServerInfo)ws).Maps.Contains(-1)) && ws.RealmId == WorldServer.Info.Realm select ws;

            var worldServer = servers.OrderBy(ws => ws.ActiveConnections).FirstOrDefault() as WorldServerInfo;

            return worldServer;
        }

        public WorldServerInfo GetWorldServer(uint realmId)
        {
            var servers = from ws in Server.WorldService.Servers.Values where ws is WorldServerInfo && ws.RealmId == WorldServer.Info.Realm select ws;
            var worldServer = servers.OrderBy(ws => ws.ActiveConnections).FirstOrDefault() as WorldServerInfo;

            return worldServer;
        }

        public ulong CreateRedirectKey(uint gameAccountId, ulong playerGuid)
        {
            var key = BitConverter.ToUInt64(new byte[0].GenerateRandomKey(8), 0);

            Server.WorldService.Update(key, Tuple.Create(gameAccountId, playerGuid));

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
            Server.WorldService.Update(key, null);
        }
    }
}

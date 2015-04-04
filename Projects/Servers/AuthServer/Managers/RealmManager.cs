// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using AuthServer.Configuration;
using Framework.Constants.Misc;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace AuthServer.Managers
{
    class RealmManager : Singleton<RealmManager>
    {
        public readonly ConcurrentDictionary<uint, Realm> RealmList;

        RealmManager()
        {
            RealmList = new ConcurrentDictionary<uint, Realm>();

            new Thread(() =>
            {
                while (true)
                {
                    Log.Debug("Updating RealmList...");
                    
                    Realm realm;
                    var realms = DB.Auth.Select<Realm>();

                    RealmList.ToList().ForEach(r =>
                    {
                        if (!realms.Any(nR => nR.Id.Equals(r.Key)))
                            if (RealmList.TryRemove(r.Key, out realm))
                                Log.Debug("Removed Realm (Id: {0}, Name: {1})", r.Key, r.Value.Name);
                    });

                    foreach (var r in realms)
                    {
                        if (RealmList.TryGetValue(r.Id, out realm))
                        {
                            RealmList.TryUpdate(r.Id, r, realm);
                            continue;
                        }

                        if (RealmList.TryAdd(r.Id, r))
                            Log.Debug("Added Realm Id: {0}, Name: {1}", r.Id, r.Name);
                    }

                    IsInitialized = true;

                    if (RealmList.IsEmpty)
                        Log.Debug("No realms available");

                    Thread.Sleep(AuthConfig.RealmListUpdateTime);
                }
            }).Start();
        }
    }
}

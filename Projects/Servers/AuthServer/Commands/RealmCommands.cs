// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Attributes;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace AuthServer.Commands
{
    class RealmCommands
    {
        [ConsoleCommand("CreateRealm", "")]
        public static void CreateRealm(string[] args)
        {
            var realmName = Command.Read<string>(args, 0);
            var realmIP   = Command.Read<string>(args, 1);
            var realmPort = Command.Read<ushort>(args, 2);

            if (realmName != "" && realmIP != "" && realmPort != 0)
            {
                var exists = DB.Auth.Any<Realm>(r => r.Name == realmName);

                if (!exists)
                {
                    var realm = new Realm
                    {
                        Id     = DB.Auth.GetAutoIncrementValue<Realm, uint>(),
                        Name   = realmName,
                        Type   = 1,
                        State  = 0,
                        Flags  = 0
                    };

                    if (DB.Auth.Add(realm))
                    {
                        // Default class/expansion data (sent in AuthResponse)
                        var defaultAllowedClasses = new byte[,] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 2 },
                                                                { 7, 0 }, { 8, 0 }, { 9, 0 }, { 10, 4 }, { 11, 0 } };

                        // Default race/expansion data (sent in AuthResponse)
                        var defaultAllowedRaces = new byte[,] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 },
                                                              { 7, 0 }, { 8, 0 }, { 9, 3 }, { 10, 1 }, { 11, 1 }, { 22, 3 },
                                                              { 24, 4 }, { 25, 4 }, { 26, 4 }};

                        for (int i = 0; i < defaultAllowedClasses.Length / 2; i++)
                        {
                            DB.Auth.Add(new RealmClass
                            {
                                RealmId   = realm.Id,
                                Class     = defaultAllowedClasses[i, 0],
                                Expansion = defaultAllowedClasses[i, 1]
                            });
                        }

                        for (int i = 0; i < defaultAllowedRaces.Length / 2; i++)
                        {
                            DB.Auth.Add(new RealmRace
                            {
                                RealmId   = realm.Id,
                                Race      = defaultAllowedRaces[i, 0],
                                Expansion = defaultAllowedRaces[i, 1]
                            });
                        }

                        Log.Normal($"Realm '{realmName}' successfully created.");
                    }
                }
                else
                    Log.Error($"Realm '{realmName}' already exists.");
            }

        }
    }
}
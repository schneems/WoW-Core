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

using Framework.Attributes;
using Framework.Constants.Misc;
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
                        Name   = realmName,
                        IP     = realmIP,
                        Port   = realmPort,
                        Type   = 1,
                        Status = 0,
                        Flags  = 0
                    };

                    if (DB.Auth.Add(realm))
                    {
                        var newRealm = DB.Auth.Single<Realm>(r => r.Name == realm.Name);

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
                                RealmId   = newRealm.Id,
                                Class     = defaultAllowedClasses[i, 0],
                                Expansion = defaultAllowedClasses[i, 1]
                            });
                        }

                        for (int i = 0; i < defaultAllowedRaces.Length / 2; i++)
                        {
                            DB.Auth.Add(new RealmRace
                            {
                                RealmId   = newRealm.Id,
                                Race      = defaultAllowedRaces[i, 0],
                                Expansion = defaultAllowedRaces[i, 1]
                            });
                        }

                        Log.Message(LogType.Normal, "Realm '{0}' successfully created.", realmName);
                    }
                }
                else
                    Log.Message(LogType.Error, "Realm '{0}' doesn't exist.", realmName);
            }

        }
    }
}
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

using System.Linq;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Misc;
using Framework.Cryptography.BNet;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace AuthServer.Commands
{
    class AccountCommands
    {
        [ConsoleCommand("CreateAccount", "")]
        public static void CreateAccount(string[] args)
        {
            var email = Command.Read<string>(args, 0);
            var password = Command.Read<string>(args, 1);

            if (email != null && password != null)
            {
                var salt = new byte[0].GenerateRandomKey(0x20).ToHexString();
                var result = DB.Auth.Accounts.Any(a => a.Email.Equals(email));

                if (!result)
                {
                    var srp = new SRP6a(salt);

                    srp.CalculateX(email, password.ToUpper(), false);

                    var account = new Account
                    {
                        Email            = email,
                        PasswordVerifier = srp.V.ToHexString(),
                        Salt             = salt,
                        Region           = Regions.XX,
                    };

                    if (DB.Auth.Add(account))
                        Log.Message(LogType.Normal, "Account {0} successfully created", email);
                }
                else
                    Log.Message(LogType.Error, "Account {0} already in database", email);
            }
        }

        [ConsoleCommand("CreateGameAccount", "")]
        public static void CreateGameAccount(string[] args)
        {
            var accountId = Command.Read<int>(args, 0);
            var game = Command.Read<string>(args, 1);
            var index = Command.Read<byte>(args, 2);

            if (accountId != 0 && game != "" && index != 0)
            {
                var account = DB.Auth.Accounts.SingleOrDefault(a => a.Id.Equals(accountId));

                if (account != null)
                {
                    var exists = account.GameAccounts != null ? account.GameAccounts.Any(ga => ga.Game == game && ga.Index == index) : false;

                    if (!exists)
                    {
                        var gameAccount = new GameAccount
                        {
                            AccountId = account.Id,
                            Game      = game,
                            Index     = index,
                            Region    = Regions.XX,
                            Flags     = GameAccountFlags.None,
                            BoxLevel  = 5
                        };

                        if (DB.Auth.Add(gameAccount))
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
                                DB.Auth.Add(new AllowedClass
                                {
                                    AccountId = gameAccount.Id,
                                    Class     = defaultAllowedClasses[i, 0],
                                    Expansion = defaultAllowedClasses[i, 1]
                                });
                            }

                            for (int i = 0; i < defaultAllowedRaces.Length / 2; i++)
                            {
                                DB.Auth.Add(new AllowedRace
                                {
                                    AccountId = gameAccount.Id,
                                    Race      = defaultAllowedRaces[i, 0],
                                    Expansion = defaultAllowedRaces[i, 1]
                                });
                            }

                            Log.Message(LogType.Normal, "GameAccount '{0}{1}' for Account '{2}' successfully created.", game, index, accountId);
                        }
                    }
                }
                else
                    Log.Message(LogType.Error, "Account '{0}' doesn't exist.", accountId);
            }
        }
    }
}

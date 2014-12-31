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
                var result = DB.Auth.Any<Account>(a => a.Email == email);

                if (!result)
                {
                    var srp = new SRP6a(salt);

                    srp.CalculateX(email, password.ToUpper(), false);

                    var account = new Account
                    {
                        Email            = email,
                        PasswordVerifier = srp.V.ToHexString(),
                        Salt             = salt,
                        Region           = Region.XX,
                    };

                    if (DB.Auth.Add(account))
                        Log.Message(LogType.Normal, "Account \{email} successfully created.");
                }
                else
                    Log.Message(LogType.Error, "Account \{email} already in database.");
            }
        }

        [ConsoleCommand("CreateGameAccount", "")]
        public static void CreateGameAccount(string[] args)
        {
            var email = Command.Read<string>(args, 0);
            var game = Command.Read<string>(args, 1);
            var index = Command.Read<byte>(args, 2);

            if (email != "" && game != "" && index != 0)
            {
                var account = DB.Auth.Single<Account>(a => a.Email == email);

                if (account != null)
                {
                    var exists = account.GameAccounts?.Any(ga => ga.Game == game && ga.Index == index) ?? false;

                    if (!exists)
                    {
                        var gameAccount = new GameAccount
                        {
                            AccountId = account.Id,
                            Game      = game,
                            Index     = index,
                            Region    = Region.XX,
                            Flags     = GameAccountFlags.None,
                            BoxLevel  = 5
                        };

                        if (DB.Auth.Add(gameAccount))
                            Log.Message(LogType.Normal, "GameAccount '\{game}\{index}' for Account '\{email}' successfully created.");
                        else
                            Log.Message(LogType.Error, "GameAccount creation '\{game}\{index}' for Account '\{email}' failed.");
                    }
                    else
                        Log.Message(LogType.Error, "GameAccount '\{game}\{index}' for Account '\{email}' already in database.");
                }
                else
                    Log.Message(LogType.Error, "Account '\{email}' doesn't exist.");
            }
        }
        
        [ConsoleCommand("DeleteAccount", "")]
        public static void DeleteAccount(string[] args)
        {
            var email = Command.Read<string>(args, 0);

            if (email != "")
            {
                var account = DB.Auth.Single<Account>(a => a.Email == email);

                if (account != null)
                {
                    if (DB.Auth.Delete(account))
                        Log.Message(LogType.Normal, "Account '\{account.Email}' successfully deleted.");
                    else
                        Log.Message(LogType.Error, "Failed to delete account '\{account.Email}'.");
                }
                else
                    Log.Message(LogType.Error, "Account '\{email}' doesn't exist.");
            }
        }
    }
}

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
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Misc;

namespace WorldServer.Managers
{
    class RedirectManager : Singleton<RedirectManager>
    {
        RedirectManager()
        {
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

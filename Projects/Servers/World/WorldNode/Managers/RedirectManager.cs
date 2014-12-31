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
using Framework.Cryptography.WoW;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Database.Character.Entities;
using Framework.Misc;

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

        public Tuple<GameAccount, Character> GetAccountInfo(ulong key)
        {
            var redirect = DB.Auth.Single<CharacterRedirect>(gar => gar.Key == key);

            if (redirect != null)
            {
                var character = DB.Character.Single<Character>(ga => ga.Guid == redirect.CharacterGuid);

                if (character != null)
                {
                    var gameAccount = DB.Auth.Single<GameAccount>(a => a.Id == character.GameAccountId);

                    if (gameAccount != null)
                        return Tuple.Create(gameAccount, character);
                }
            }

            return null;
        }

        public bool DeleteCharacterRedirect(ulong key)
        {
            return DB.Auth.Delete<CharacterRedirect>(gar => gar.Key == key);
        }
    }
}

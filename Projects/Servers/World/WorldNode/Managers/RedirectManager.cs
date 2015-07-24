// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

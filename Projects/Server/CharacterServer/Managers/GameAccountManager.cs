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

using System.Collections.Generic;
using System.Linq;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Database.Character.Entities;
using Framework.Misc;

namespace CharacterServer.Managers
{
    class GameAccountManager : Singleton<GameAccountManager>
    {
        GameAccountManager()
        {
        }

        public Dictionary<byte, byte> GetAvailableRaces(GameAccount gameAccount, Realm realm)
        {
            var races = new Dictionary<byte, byte>();

            if (gameAccount.GameAccountRaces != null)
                gameAccount.GameAccountRaces.ToList().ForEach(gar => races.Add(gar.Race, gar.Expansion));
            else if (realm.RealmRaces != null)
                realm.RealmRaces.ToList().ForEach(rr => races.Add(rr.Race, rr.Expansion));

            return races;
        }

        public Dictionary<byte, byte> GetAvailableClasses(GameAccount gameAccount, Realm realm)
        {
            var classes = new Dictionary<byte, byte>();
    
            if (gameAccount.GameAccountClasses != null)
                gameAccount.GameAccountClasses.ToList().ForEach(gac => classes.Add(gac.Class, gac.Expansion));
            else if (realm.RealmClasses != null)
                realm.RealmClasses.ToList().ForEach(rc => classes.Add(rc.Class, rc.Expansion));

            return classes;
        }

        public List<CharacterTemplateSet> GetAvailableCharacterTemplates(GameAccount gameAccount, Realm realm)
        {
            var charTemplates = new List<CharacterTemplateSet>();

            if (gameAccount.GameAccountCharacterTemplates != null)
            {
                gameAccount.GameAccountCharacterTemplates.ToList().ForEach(gat =>
                {
                    var set = DB.Character.Single<CharacterTemplateSet>(cts => cts.Id == gat.SetId);

                    if (set != null)
                        charTemplates.Add(set);
                });
            }
            else
            {
                if (realm.RealmCharacterTemplates != null)
                {
                    realm.RealmCharacterTemplates.ToList().ForEach(rt =>
                    {
                        var set = DB.Character.Single<CharacterTemplateSet>(cts => cts.Id == rt.SetId);

                        if (set != null)
                            charTemplates.Add(set);
                    });
                }
            }

            return charTemplates;
        }
    }
}

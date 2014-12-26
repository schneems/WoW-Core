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
using CharacterServer.Packets.Structures.Character;
using CharacterServer.Packets.Structures.Misc;
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

        public List<RaceClassAvailability> GetAvailableRaces(GameAccount gameAccount, Realm realm)
        {
            var races = new List<RaceClassAvailability>();

            if (gameAccount.GameAccountRaces != null)
            {
                gameAccount.GameAccountRaces.ToList().ForEach(gar =>
                {
                    races.Add(new RaceClassAvailability { RaceOrClassID = gar.Race, RequiredExpansion = gar.Expansion });
                });
            }
            else
            {
                realm.RealmRaces?.ToList().ForEach(rr =>
                {
                    races.Add(new RaceClassAvailability { RaceOrClassID = rr.Race, RequiredExpansion = rr.Expansion });
                });
            }

            return races;
        }

        public List<RaceClassAvailability> GetAvailableClasses(GameAccount gameAccount, Realm realm)
        {
            var classes = new List<RaceClassAvailability>();

            if (gameAccount.GameAccountClasses != null)
            {
                gameAccount.GameAccountClasses.ToList().ForEach(gac =>
                {
                    classes.Add(new RaceClassAvailability { RaceOrClassID = gac.Class, RequiredExpansion = gac.Expansion });
                });
            }
            else
            {
                realm.RealmClasses?.ToList().ForEach(rc =>
                {
                    classes.Add(new RaceClassAvailability { RaceOrClassID = rc.Class, RequiredExpansion = rc.Expansion });
                });
            }

            return classes;
        }

        public List<AvailableCharacterTemplateSet> GetAvailableCharacterTemplates(GameAccount gameAccount, Realm realm)
        {
            var charTemplates = new List<AvailableCharacterTemplateSet>();

            if (gameAccount.GameAccountCharacterTemplates != null)
            {
                gameAccount.GameAccountCharacterTemplates.ToList().ForEach(gat =>
                {
                    var set = DB.Character.Single<CharacterTemplateSet>(cts => cts.Id == gat.SetId);

                    if (set != null)
                    {
                        var templateSet = new AvailableCharacterTemplateSet
                        {
                            TemplateSetID = (uint)set.Id,
                            Name          = set.Name,
                            Description   = set.Description,
                        };

                        set.CharacterTemplateClasses.ToList().ForEach(ctc =>
                        {
                            templateSet.Classes.Add(new AvailableCharacterTemplateClass
                            {
                                ClassID      = ctc.ClassId,
                                FactionGroup = ctc.FactionGroup
                            });
                        });

                        charTemplates.Add(templateSet);
                    }
                });
            }
            else
            {
                realm.RealmCharacterTemplates?.ToList().ForEach(rt =>
                {
                    var set = DB.Character.Single<CharacterTemplateSet>(cts => cts.Id == rt.SetId);

                    var templateSet = new AvailableCharacterTemplateSet
                    {
                        TemplateSetID = (uint)set.Id,
                        Name          = set.Name,
                        Description   = set.Description,
                    };

                    set.CharacterTemplateClasses.ToList().ForEach(ctc =>
                    {
                        templateSet.Classes.Add(new AvailableCharacterTemplateClass
                        {
                            ClassID      = ctc.ClassId,
                            FactionGroup = ctc.FactionGroup
                        });
                    });

                    charTemplates.Add(templateSet);
                });
            }

            return charTemplates;
        }
    }
}

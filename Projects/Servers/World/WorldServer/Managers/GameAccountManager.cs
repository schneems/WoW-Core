// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using WorldServer.Packets.Structures.Character;
using WorldServer.Packets.Structures.Misc;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Database.Character.Entities;
using Framework.Misc;

namespace WorldServer.Managers
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

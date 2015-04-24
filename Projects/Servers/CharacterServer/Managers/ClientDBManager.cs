// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Framework.Database;
using Framework.Database.Data.Entities;
using Framework.Datastore;
using Framework.Logging;
using Framework.Misc;

namespace CharacterServer.Managers
{
    class ClientDBManager : Singleton<ClientDBManager>
    {
        ClientDBManager()
        {
            InitializeStorage();
        }

        void InitializeStorage()
        {
            Log.Message();
            Log.Normal("Initialize ClientDB storages...");

            ClientDB.CharBaseInfo            = DB.Data.Select<CharBaseInfo>();
            ClientDB.CharStartOutfits        = DB.Data.Select<CharStartOutfit>();
            ClientDB.ChrClasses              = DB.Data.Select<ChrClass>();
            ClientDB.ChrRaces                = DB.Data.Select<ChrRace>();
            ClientDB.ItemModifiedAppearances = DB.Data.Select<ItemModifiedAppearance>().ToLookup(ima => ima.ItemId) as Lookup<int, ItemModifiedAppearance>;
            ClientDB.ItemAppearances         = DB.Data.Select<uint, ItemAppearance>(ia => ia.Id);
            ClientDB.Items                   = DB.Data.Select<uint, Item>(i => i.Id);
            ClientDB.NameGens                = DB.Data.Select<NameGen>();
            ClientDB.SkillLines              = DB.Data.Select<uint, SkillLine>(sl => sl.ID);
            ClientDB.SkillLineAbilities      = DB.Data.Select<SkillLineAbility>();
            ClientDB.SkillRaceClassInfo      = DB.Data.Select<SkillRaceClassInfo>();
            ClientDB.SpellLevels             = DB.Data.Select<SpellLevels>().ToLookup(sl => sl.SpellId) as Lookup<uint, SpellLevels>;

            // Load player experience levels (first 100 entries) from game table.
            ClientDB.GtOCTLevelExperience = DB.Data.Where<GtOCTLevelExperience>(gt => gt.Index < 100);

            Log.Normal("ClientDB storages successfully initialized.");
            Log.Message();
        }
    }
}

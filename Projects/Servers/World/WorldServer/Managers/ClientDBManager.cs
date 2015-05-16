// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Framework.Database;
using Framework.Database.Data.Entities;
using Framework.Datastore;
using Framework.Logging;
using Framework.Misc;

namespace WorldServer.Managers
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

            ClientDB.ChrClasses              = DB.Data.Select<ChrClass>();
            ClientDB.ChrRaces                = DB.Data.Select<ChrRace>();
            ClientDB.GameTables              = DB.Data.Select<GameTables>();
            ClientDB.ItemModifiedAppearances = DB.Data.Select<ItemModifiedAppearance>().ToLookup(ima => ima.ItemId) as Lookup<int, ItemModifiedAppearance>;
            ClientDB.ItemAppearances         = DB.Data.Select<uint, ItemAppearance>(ia => ia.Id);
            ClientDB.Items                   = DB.Data.Select<uint, Item>(i => i.Id);
            ClientDB.SkillLines              = DB.Data.Select<uint, SkillLine>(sl => sl.ID);
            ClientDB.SkillLineAbilities      = DB.Data.Select<SkillLineAbility>();

            ClientDB.GtOCTLevelExperience = new ClientGameTable<GtOCTLevelExperience>(gt => gt.Index);

            Log.Normal("ClientDB storages successfully initialized.");
            Log.Message();
        }
    }
}

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
using Framework.Constants.Misc;
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
            Log.Message(LogType.Normal, "Initialize ClientDB storages...");

            ClientDB.ChrClasses              = DB.Data.Select<ChrClass>();
            ClientDB.ChrRaces                = DB.Data.Select<ChrRace>();
            ClientDB.ItemModifiedAppearances = DB.Data.Select<ItemModifiedAppearance>().ToLookup(ima => ima.ItemId) as Lookup<int, ItemModifiedAppearance>;
            ClientDB.ItemAppearances         = DB.Data.Select<uint, ItemAppearance>(ia => ia.Id);
            ClientDB.Items                   = DB.Data.Select<uint, Item>(i => i.Id);
            ClientDB.SkillLines              = DB.Data.Select<uint, SkillLine>(sl => sl.ID);
            ClientDB.SkillLineAbilities      = DB.Data.Select<SkillLineAbility>();

            Log.Message(LogType.Normal, "ClientDB storages successfully initialized.");
            Log.Message();
        }
    }
}
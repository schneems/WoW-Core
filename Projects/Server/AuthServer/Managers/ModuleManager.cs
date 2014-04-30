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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework.Constants.Misc;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;

namespace AuthServer.Managers
{
    class ModuleManager : Singleton<ModuleManager>
    {
        public readonly List<Module> ModuleList;
        public readonly List<Module> Module64List;
        public readonly List<Module> ModuleMacList;

        ModuleManager()
        {
            ModuleList = new List<Module>();
            Module64List = new List<Module>();
            ModuleMacList = new List<Module>();

            UpdateModules();
        }

        public void UpdateModules()
        {
            Log.Message(LogType.Debug, "Loading auth modules...");

            var modules = DB.Auth.Modules.Select(m => m);

            foreach (var m in modules)
            {
                if (m.System == "Win" && AddModule(m, ModuleList))
                    Log.Message(LogType.Debug, "New Win auth module '{0}' loaded", m.Hash);
                else if (m.System == "Wn64" && AddModule(m, Module64List))
                    Log.Message(LogType.Debug, "New Win64 auth module '{0}' loaded", m.Hash);
                else if (m.System == "Mc64" && AddModule(m, ModuleMacList))
                    Log.Message(LogType.Debug, "New Mac64 auth module '{0}' loaded", m.Hash);
            }

            Log.Message(LogType.Debug, "Successfully loaded {0} auth modules", ModuleList.Count);
        }

        bool AddModule(Module module, IList list)
        {
            if (!list.Contains(module))
                return list.Add(module) != -1;

            return false;
        }
    }
}

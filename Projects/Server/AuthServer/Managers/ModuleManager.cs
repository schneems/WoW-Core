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
using AuthServer.Network;
using Framework.Constants.Misc;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;

namespace AuthServer.Managers
{
    class ModuleManager : Singleton<ModuleManager>
    {
        public readonly List<Module> Modules;

        ModuleManager()
        {
            Modules = new List<Module>();

            UpdateModules();
        }

        public void UpdateModules()
        {
            Log.Message(LogType.Debug, "Loading auth modules...");

            var modules = DB.Auth.Modules.Select(m => m);

            foreach (var m in modules)
                if (AddModule(m, Modules))
                    Log.Message(LogType.Debug, "New auth module '{0}' loaded", m.Hash);

            Log.Message(LogType.Debug, "Successfully loaded {0} auth modules", Modules.Count);

            IsInitialized = true;
        }

        public void WriteModuleHeader(AuthSession session, AuthPacket packet, Module module, int size = 0)
        {
            packet.WriteFourCC(module.Type);
            packet.WriteFourCC("\0\0" + session.Account.Region);
            packet.Write(module.Hash.ToByteArray());
            packet.Write(size == 0 ? module.Size : size, 10);
        }

        bool AddModule(Module module, IList list)
        {
            if (!list.Contains(module))
                return list.Add(module) != -1;

            return false;
        }
    }
}

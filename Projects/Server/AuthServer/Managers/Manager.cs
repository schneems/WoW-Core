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

namespace AuthServer.Managers
{
    class Manager
    {
        public static ModuleManager Module;
        public static RealmManager Realm;

        public static void Initialize()
        {
            Module = ModuleManager.GetInstance();
            Realm  = RealmManager.GetInstance();
        }

        public static bool GetState()
        {
            var state = false;
            var nullState = Module != null && Realm != null;

            if (nullState)
                state = nullState && (Module.IsInitialized && Realm.IsInitialized);

            return state;
        }
    }
}

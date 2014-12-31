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

using System;
using System.Collections.Concurrent;
using Framework.Objects.WorldEntities;

namespace Framework.Network.Remoting
{
    public class RemoteObject : MarshalByRefObject
    {
        public ConcurrentDictionary<ulong, Player> Players;

        public RemoteObject()
        {
            Players = new ConcurrentDictionary<ulong, Player>();
        }

        public bool Add(ulong key, Player value)
        {
            return Players.TryAdd(key, value);
        }

        public bool Remove(ulong key, Player value)
        {
            return Players.TryRemove(key, out value);
        }

        public Player Update(Player newValue)
        {
            return Players.AddOrUpdate(newValue.Data.Guid, newValue, (key, oldValue) => newValue);
        }

        public Player GetValue(ulong key)
        {
            Player value;

            if (Players.TryGetValue(key, out value))
                return value;

            return null;
        }
    }
}

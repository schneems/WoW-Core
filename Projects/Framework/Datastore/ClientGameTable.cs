// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Database;
using Framework.Database.Data.Entities;
using Framework.Logging;
using Lappa_ORM;

namespace Framework.Datastore
{
    public class ClientGameTable<T> where T : Entity, new()
    {
        public T this[long column, long row]
        {
            get
            {
                if (gameTable == null || column >= gameTable.NumColumns || row > gameTable.NumRows)
                    return null;

                Dictionary<int, T> values;
                T value;

                if (data.TryGetValue((int)column, out values) && values.TryGetValue((int)row - 1, out value))
                    return value;

                return null;
            }
        }

        readonly GameTables gameTable;
        readonly Dictionary<int, Dictionary<int, T>> data;

        public ClientGameTable(Func<T, int> func)
        {
            gameTable = ClientDB.GameTables.SingleOrDefault(gt => gt.Name == typeof(T).Name.Substring(2));

            if (gameTable != null)
            {
                var tempData = DB.Data.Select(func).OrderBy(kp => kp.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

                if (tempData.Count != gameTable.NumColumns * gameTable.NumRows)
                {
                    Log.Error($"Wrong data for {gameTable.Name}.");

                    return;
                }

                data = new Dictionary<int, Dictionary<int, T>>(gameTable.NumColumns);

                for (var i = 0; i < gameTable.NumColumns; i++)
                {
                    data[i] = tempData.Values.Take(gameTable.NumRows).ToDictionary(func);

                    tempData = tempData.Skip(gameTable.NumRows).ToDictionary(pair => pair.Key, pair => pair.Value);
                }
            }
            else
                Log.Error($"No GameTables data for {typeof(T).Name}.");
        }
    }
}

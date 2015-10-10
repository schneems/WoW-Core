// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Framework.Logging;

namespace Framework.Misc
{
    public class Config
    {
        string[] configContent;

        public Config(string config)
        {
            if (!File.Exists(config))
            {
                Log.Error($"{config} doesn't exist!");
                Log.Wait();

                Environment.Exit(0);
            }
            else
                configContent = File.ReadAllLines(config, Encoding.UTF8);
        }

        public T Read<T>(string name, T value, bool hex = false)
        {
            string nameValue = null;
            var lineCounter = 0;

            try
            {
                if (configContent.Length != 0)
                {
                    foreach (var option in configContent)
                    {
                        var configOption = option.Split(new char[] { '=' }, StringSplitOptions.None);

                        if (configOption[0].StartsWith(name, StringComparison.Ordinal))
                        {
                            if (configOption[1].Trim() == "")
                                nameValue = value.ToString();
                            else
                                nameValue = configOption[1].Replace("\"", "").Trim();
                        }

                        lineCounter++;
                    }
                }
                else
                {
                    nameValue = value.ToString();

                    Log.Error($"Can't find config option '{name}'");
                    Log.Message($"Use default value '{value}'");
                    Log.Message();
                }

                if (hex)
                    return Convert.ToInt32(nameValue, 16).ChangeType<T>();

                if (typeof(T) == typeof(bool))
                {
                    if (nameValue == "0")
                        return false.ChangeType<T>();
                    else if (nameValue == "1")
                        return true.ChangeType<T>();
                }

            }
            catch
            {
                Log.Error($"Error while reading config option: '{name}'");
            }

            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), nameValue, true);
            else
                return nameValue.ChangeType<T>();
        }

        public Dictionary<uint, ServerInfo> ReadServerDefinitions(string prefix)
        {
            var ret = new Dictionary<uint, ServerInfo>();

            foreach (var option in configContent)
            {
                if (option.StartsWith(prefix))
                {
                    var id = Regex.Match(option, $"{prefix}(.?).?=").Groups[1].Captures[0].Value;
                    var data = Regex.Match(option, "{(.*}?)\\}").Groups[1].Captures[0].Value;
                    var mapData = Regex.IsMatch(data, "{(.*}?)\\}") ? Regex.Match(data, "{(.*}?)\\}").Groups[1].Captures[0].Value : "";
                    var values = (mapData != "" ? data.Replace(mapData, "") : data).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToArray();

                    var serverInfo = new ServerInfo { Realm = uint.Parse(values[0]) };

                    if (values.Length == 4)
                    {
                        var mapValues = mapData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        serverInfo.Maps = new int[mapValues.Length];

                        for (var i = 0; i < mapValues.Length; i++)
                            serverInfo.Maps[i] = int.Parse(mapValues[i]);

                        serverInfo.Address = values[2].Replace("\"", "");
                        serverInfo.Port = ushort.Parse(values[3]);
                    }
                    else
                    {
                        serverInfo.Address = values[1].Replace("\"", "");
                        serverInfo.Port = ushort.Parse(values[2]);
                    }

                    ret.Add(uint.Parse(id), serverInfo);
                }
            }

            return ret;
        }
    }
}

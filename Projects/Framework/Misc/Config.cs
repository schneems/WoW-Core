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

using System;
using System.IO;
using System.Text;
using Framework.Constants.Misc;
using Framework.Logging;

namespace Framework.Misc
{
    public class Config
    {
        string[] configContent;
        string ConfigFile { get; set; }

        public Config(string config)
        {
            ConfigFile = config;

            if (!File.Exists(config))
            {
                Log.Message(LogType.Error, "{0} doesn't exist!", config);
                Log.Wait();

                Environment.Exit(0);
            }
            else
                configContent = File.ReadAllLines(config, Encoding.UTF8);
        }

        public T Read<T>(string name, T value, bool hex = false)
        {
            string nameValue = null;
            T trueValue = (T)Convert.ChangeType(value, typeof(T));
            var lineCounter = 0;

            try
            {
                if (configContent.Length != 0)
                {

                    foreach (var option in configContent)
                    {
                        var configOption = option.Split(new char[] { '=' }, StringSplitOptions.None);
                        if (configOption[0].StartsWith(name, StringComparison.Ordinal))
                            if (configOption[1].Trim() == "")
                                nameValue = value.ToString();
                            else
                                nameValue = configOption[1].Replace("\"", "").Trim();

                        lineCounter++;
                    }
                }
                else
                {
                    nameValue = value.ToString();

                    Log.Message(LogType.Error, "Can't find config option '{0}'", name);
                    Log.Message(LogType.None, "Use default value '{0}'", value);
                    Log.Message();
                }

                if (hex)
                    return (T)Convert.ChangeType(Convert.ToInt32(nameValue, 16), typeof(T));

                if (typeof(T) == typeof(bool))
                {
                    if (nameValue == "0")
                        return (T)Convert.ChangeType(false, typeof(T));
                    else if (nameValue == "1")
                        return (T)Convert.ChangeType(true, typeof(T));
                }

            }
            catch
            {
                Log.Message(LogType.Error, "Error while reading config option: '{0}'", name);
            }

            return (T)Convert.ChangeType(nameValue, typeof(T));
        }
    }
}

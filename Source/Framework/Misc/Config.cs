// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Framework.Misc
{
    public class Config
    {
        string[] configContent;
        int currentLine;

        public Config(string config, bool isChild = false)
        {
            if (!File.Exists(config))
            {
                if (!isChild)
                {
                    // Use default console logging
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{config} doesn't exist!");

                    Console.ReadKey(true);
                }

                Environment.Exit(0);
            }
            else
                configContent = File.ReadAllLines(config, Encoding.UTF8);

            currentLine = 0;
        }

        public T Read<T>(string name, T value)
        {
            var nameValue = "";

            try
            {
                if (configContent.Length != 0)
                {
                    var foundOption = false;

                    for (; currentLine < configContent.Length; currentLine++)
                    {
                        var configOption = configContent[currentLine].Split(new[] { '=' }, StringSplitOptions.None);

                        if (configOption[0].Trim().StartsWith(name, StringComparison.Ordinal))
                        {
                            foundOption = true;

                            if (configOption[1].Trim() == "")
                                nameValue = value.ToString();
                            else
                                nameValue = configOption[1].Replace("\"", "").Trim();

                            break;
                        }
                    }

                    if (!foundOption)
                    {
                        currentLine = 0;
                        nameValue = value.ToString();

                        // Use default console logging
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Can't find config option '{name}'");

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Using default value '{value}'");

                        Console.WriteLine();
                    }
                }

                if (typeof(T) == typeof(bool))
                    return Convert.ToBoolean(nameValue).ChangeType<T>();

                // Primitive types & numeric/string enum options.
                if (typeof(T).GetTypeInfo().IsPrimitive || typeof(T).GetTypeInfo().IsEnum)
                {
                    // Check for hex numbers (starting with 0x).
                    var numberBase = nameValue.StartsWith("0x") ? 16 : 10;

                    // Parse enum options by string.
                    if (typeof(T).GetTypeInfo().IsEnum && numberBase == 10)
                        return (T)Enum.Parse(typeof(T), nameValue);

                    // Get the true type.
                    var valueType = typeof(T).GetTypeInfo().IsEnum ? typeof(T).GetTypeInfo().GetEnumUnderlyingType() : typeof(T);

                    // Check if it's a signed or unsigned type and convert/cast it to the correct type.
                    return valueType.IsSigned() ? Convert.ToInt64(nameValue, numberBase).ChangeType<T>() : Convert.ToUInt64(nameValue, numberBase).ChangeType<T>();
                }
            }
            catch
            {
                // Use default console logging
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Can't read config option '{name}' on line '{currentLine}'.");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Using default value '{value}'");

                Console.WriteLine();

                return value.ChangeType<T>();
            }

            // String options.
            return nameValue.ChangeType<T>();
        }
    }
}

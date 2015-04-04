// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ConsoleCommandAttribute : Attribute
    {
        public string Command { get; }
        public string Description { get; }

        public ConsoleCommandAttribute(string command, string description)
        {
            Command = command.ToLower();
            Description = description;
        }
    }
}

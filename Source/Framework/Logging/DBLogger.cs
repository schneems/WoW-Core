// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using LappaORM.Logging;

namespace Framework.Logging
{
    public class DBLogger : Logger, ILog
    {
        public void Message(Enum logTypes, string message)
        {
            Message((LogTypes)Enum.Parse(typeof(LogTypes), logTypes.ToString()), message);
        }
    }
}

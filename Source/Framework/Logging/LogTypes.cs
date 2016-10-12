// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Logging
{
    [Flags]
    public enum LogTypes
    {
        None    = 0x0,
        Success = 0x1,
        Info    = 0x2,
        Warning = 0x4,
        Error   = 0x8,
        Input   = 0x10
    }
}

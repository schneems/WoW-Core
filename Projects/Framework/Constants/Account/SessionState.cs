// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Constants.Account
{
    [Flags]
    public enum SessionState
    {
        None          = 0,
        Initiated     = 1,
        Authenticated = 2,
        Redirected    = 4,
        InWorld       = 8,
        All           = Initiated | Authenticated | Redirected | InWorld,
    }
}

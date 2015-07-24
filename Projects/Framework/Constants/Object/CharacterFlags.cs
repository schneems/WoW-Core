// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Constants.Object
{
    [Flags]
    public enum CharacterFlags : uint
    {
        None    = 0,
        Decline = 0x2000000,
    }
}

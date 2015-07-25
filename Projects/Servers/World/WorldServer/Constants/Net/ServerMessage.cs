// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    enum ServerMessage : ushort
    {
        AccountDataTimes   = 0x000C,
        InitialKnownSpells = 0x096E,
        ObjectUpdate       = 0x0D36,
    }
}

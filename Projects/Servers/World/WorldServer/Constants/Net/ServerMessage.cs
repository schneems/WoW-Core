// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    enum ServerMessage : ushort
    {
        AccountDataTimes   = 0x09FB,
        InitialKnownSpells = 0x1E0A,
        ObjectUpdate       = 0x0C59,
    }
}

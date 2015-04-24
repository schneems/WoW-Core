// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    enum ServerMessage : ushort
    {
        AccountDataTimes   = 0x0E32,
        InitialKnownSpells = 0x096E,
        ObjectUpdate       = 0x02A5,
    }
}

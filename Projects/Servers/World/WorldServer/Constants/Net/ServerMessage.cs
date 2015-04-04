// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    enum ServerMessage : ushort
    {
        AccountDataTimes = 0x16B8,
        ObjectUpdate     = 0x1CB2,
    }
}

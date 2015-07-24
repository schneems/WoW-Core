// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Constants.Authentication
{
    public enum AuthResult : byte
    {
        GlobalSuccess       = 0x00,
        InternalError       = 0x64,
        CorruptedModule     = 0x65,
        BadLoginInformation = 0x68,
        InvalidProgram      = 0x6D,
        InvalidPlatform     = 0x6E,
        InvalidLocale       = 0x6F,
        InvalidGameVersion  = 0x70,
        ServerBusy          = 0x71,
        NoGameAccount       = 0xC9,
        AlreadyLoggedIn     = 0xCD,
    }
}

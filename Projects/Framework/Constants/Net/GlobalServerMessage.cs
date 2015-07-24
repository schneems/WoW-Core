// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalServerMessage : ushort
    {
        AuthChallenge           = 0x1102,
        ConnectTo               = 0x1101,
        SuspendComms            = 0x1105,
        ResumeComms             = 0x1302,
        Compression             = 0x1806,
        ResetCompressionContext = 0x1006,
        Composite               = 0x1206,
        Pong                    = 0x1805,
        CharacterLoginFailed    = 0x000C
    }
}

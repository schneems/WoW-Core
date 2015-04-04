// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalServerMessage : ushort
    {
        AuthChallenge        = 0x007E,
        ConnectTo            = 0x0119,
        SuspendComms         = 0x001E,
        ResumeComms          = 0x003A,
        Pong                 = 0x2000,
        CharacterLoginFailed = 0x0FBD
    }
}

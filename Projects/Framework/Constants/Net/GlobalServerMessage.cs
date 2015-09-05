// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalServerMessage : ushort
    {
        AuthChallenge           = 0x1028,
        ConnectTo               = 0x0A27,
        SuspendComms            = 0x0A38,
        ResumeComms             = 0x1037,
        Compression             = 0x0827,
        ResetCompressionContext = 0x0228,
        Composite               = 0x1038,
        Pong                    = 0x0237,
        CharacterLoginFailed    = 0x0BA2 
    }
}

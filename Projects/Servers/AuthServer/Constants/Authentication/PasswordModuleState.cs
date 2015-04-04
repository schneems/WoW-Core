// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Constants.Authentication
{
    enum PasswordModuleState : byte
    {
        ServerChallenge = 0,
        ClientChallenge = 1,
        ClientProof     = 2,
        ValidateProof   = 3
    }
}

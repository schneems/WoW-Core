// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Constants.Net
{
    public enum AuthServerMessage : ushort
    {
        #region Authentication
        Complete              = (0x00 + 0x3F) << AuthChannel.Authentication,
        ProofRequest          = (0x02 + 0x3F) << AuthChannel.Authentication,
        #endregion
        #region Connection
        Pong                  = (0x00 + 0x3F) << AuthChannel.Connection,
        #endregion
        #region WoWRealm
        ListSubscribeResponse = (0x00 + 0x3F) << AuthChannel.WoWRealm,
        ListUpdate            = (0x02 + 0x3F) << AuthChannel.WoWRealm,
        ListComplete          = (0x03 + 0x3F) << AuthChannel.WoWRealm,
        JoinResponse          = (0x08 + 0x3F) << AuthChannel.WoWRealm
        #endregion
    }
}

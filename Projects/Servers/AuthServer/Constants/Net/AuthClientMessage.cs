// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Constants.Net
{
    public enum AuthClientMessage : ushort
    {
        #region Authentication
        ProofResponse        = (0x02 + 0x3F) << AuthChannel.Authentication,
        InformationRequest   = (0x09 + 0x3F) << AuthChannel.Authentication,
        #endregion
        #region Connection
        Ping                 = (0x00 + 0x3F) << AuthChannel.Connection,
        Disconnect           = (0x06 + 0x3F) << AuthChannel.Connection,
        #endregion
        #region WoWRealm
        ListSubscribeRequest = (0x00 + 0x3F) << AuthChannel.WoWRealm,
        JoinRequest          = (0x08 + 0x3F) << AuthChannel.WoWRealm,
        #endregion
        #region HTTP
        Receive              = 0x8C0,
        #endregion
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalClientMessage : ushort
    {
        #region UserRouterClient
        SuspendCommsAck      = 0x1375,
        AuthSession          = 0x03DD,
        AuthContinuedSession = 0x0376,
        Ping                 = 0x12DE,
        LogDisconnect        = 0x12D5,
        #endregion

        PlayerLogin          = 0x0E98,
    }
}

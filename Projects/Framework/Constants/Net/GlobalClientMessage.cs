// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalClientMessage : ushort
    {
        #region UserRouterClient
        SuspendCommsAck      = 0x0C99,
        AuthSession          = 0x045A,
        AuthContinuedSession = 0x06DA,
        Ping                 = 0x0659,
        LogDisconnect        = 0x045D,
        #endregion

        PlayerLogin          = 0x0921,
    }
}

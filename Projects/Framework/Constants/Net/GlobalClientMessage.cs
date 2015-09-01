// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalClientMessage : ushort
    {
        #region UserRouterClient
        SuspendCommsAck      = 0x092E,
        AuthSession          = 0x116D,
        AuthContinuedSession = 0x1926,
        Ping                 = 0x0930,
        LogDisconnect        = 0x117E,
        #endregion

        PlayerLogin          = 0x03F9,
    }
}

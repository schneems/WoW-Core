// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Framework.Constants.IPC
{
    public enum IPCMessage : byte
    {
        ExitProcess     = 0x00,
        RegisterConsole = 0x01,
        AttachConsole   = 0x02,
        DetachConsole   = 0x03
    }
}

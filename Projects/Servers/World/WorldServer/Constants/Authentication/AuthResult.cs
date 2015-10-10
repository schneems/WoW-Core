// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Constants.Authentication
{
    public enum AuthResult : byte
    {
        Ok                  = 0xC,
        Failed              = 0xD,
        Reject              = 0xE,
        BadServerProof      = 0xF,
        Unavailable         = 0x10,
        SystemError         = 0x11,
        BillingError        = 0x12,
        BillingExpired      = 0x13,
        VersionMismatch     = 0x14,
        UnknownAccount      = 0x15,
        IncorrectPassword   = 0x16,
        SessionExpired      = 0x17,
        ServerShuttingDown  = 0x18,
        AlreadyLoggingIn    = 0x19,
        LoginServerNotFound = 0x1A,
        WaitQueue           = 0x1B,
        Banned              = 0x1C,
        AlreadyOnline       = 0x1D,
        NoTime              = 0x1E,
        DbBusy              = 0x1F,
        Suspended           = 0x20,
        ParentalControl     = 0x21,
        LockedEnforced      = 0x22
    }
}

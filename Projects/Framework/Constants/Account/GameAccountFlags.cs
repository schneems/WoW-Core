// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Constants.Account
{
    [Flags]
    public enum GameAccountFlags : ulong
    {
        None                = 0x0,
        Trial               = 0x1,
        Restricted          = 0x2,
        Gm                  = 0x4,
        Nokick              = 0x8,
        Collector           = 0x10,
        None2               = 0x20,
        WowTrial            = 0x40,
        Cancelled           = 0x80,
        Igr                 = 0x100,
        Wholesaler          = 0x200,
        Privileged          = 0x400,
        EuForbidBilling     = 0x800,
        WowRestricted       = 0x1000,
        Referral            = 0x2000,
        Blizzard            = 0x4000,
        RecurringBilling    = 0x8000,
        Noelectup           = 0x10000,
        KrCertificate       = 0x20000,
        ExpansionCollector  = 0x40000,
        DisableVoice        = 0x80000,
        DisableVoiceSpeak   = 0x100000,
        ReferralResurrect   = 0x200000,
        EuForbidCc          = 0x400000,
        OpenbetaDell        = 0x800000,
        Propass             = 0x1000000,
        PropassLock         = 0x2000000,
        PendingUpgrade      = 0x4000000,
        RetailFromTrial     = 0x8000000,
        Expansion2Collector = 0x10000000,
        OvermindLinked      = 0x20000000,
        Demos               = 0x40000000,
        DeathKnightOk       = 0x80000000,
        S2RequireIgr        = 0x100000000,
        S2AccountUpdated    = 0x200000000,
        S2Trial             = 0x400000000,
        S2Restricted        = 0x800000000,
        S2CreatedOnDemand   = 0x1000000000
    }
}

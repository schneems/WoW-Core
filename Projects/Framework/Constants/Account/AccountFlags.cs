/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace Framework.Constants.Account
{
    public enum AccountFlags : ulong
    {
        None                                 = 0x0,
        Incomplete                           = 0x1,
        MailVerified                         = 0x2,
        Employee                             = 0x4,
        Admin                                = 0x8,
        SupportEmployee                      = 0x10,
        Newsletter                           = 0x20,
        ParentAgreementKr                    = 0x40,
        InsiderSmsKr                         = 0x80,
        CancelledKr                          = 0x100,
        BetaSignUp                           = 0x200,
        PurchaseBan                          = 0x400,
        LegalAccept                          = 0x800,
        Press                                = 0x1000,
        OneTimeEvent                         = 0x2000,
        TrialIncomplete                      = 0x4000,
        MarketingEmail                       = 0x8000,
        KrAgeNotVerified                     = 0x10000,
        SecLockMustRelease                   = 0x20000,
        SecLocked                            = 0x40000,
        RidDisabled                          = 0x80000,
        DontShowToDirectFriend               = 0x100000,
        ParentalControl                      = 0x200000,
        ParentAgree14Kr                      = 0x400000,
        PlaySummaryEmail                     = 0x800000,
        PrivNetworkRqd                       = 0x1000000,
        PrimaryRidInviteRequiresEmployeeFlag = 0x2000000,
        VoiceChatDisabled                    = 0x4000000,
        VoiceSpeakDisabled                   = 0x8000000,
        HideFromFacebookFriendFinder         = 0x10000000,
        EuForbidElv                          = 0x20000000,
        PhoneSecureEnhanced                  = 0x40000000
    }
}

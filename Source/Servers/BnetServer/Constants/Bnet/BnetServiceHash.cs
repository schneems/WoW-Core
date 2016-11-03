// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BnetServer.Constants.Bnet
{
    public enum BnetServiceHash : uint
    {
        None                          = 0x0,
        AccountService                = 0x62DA0891,
        AuthenticationServerService   = 0x0DECFC01,
        AuthenticationClientService   = 0xBBDA171F,
        AuthenticationListenerService = 0x71240E35,
        ChallengeService              = 0xDBBF6F19,
        ChannelService                = 0xB732DB32,
        ConnectionService             = 0x65446991,
        FriendsService                = 0xA3DDB1BD,
        GameUtilitiesService          = 0x3FC1274D,
        PresenceService               = 0xFA0796FF,
        ReportService                 = 0x7CAF61C9,
        ResourcesService              = 0xECBE75BA,
        UserManagerService            = 0x3E19268A
    }
}

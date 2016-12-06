// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Bgs.Protocol.Account.V1;
using BnetServer.Attributes;
using BnetServer.Constants.Bnet;
using BnetServer.Network;

namespace BnetServer.Packets.Services
{
    [BnetService(Hash = BnetServiceHash.AccountService)]
    class AccountService
    {
        [BnetMethod(MethodId = 30)]
        public static Task HandleGetAccountStateRequest(GetAccountStateRequest getAccountStateRequest, BnetSession session)
        {
            return session.Send(new GetAccountStateResponse
            {
                State = new AccountState
                {
                    PrivacyInfo = new PrivacyInfo
                    {
                        IsHiddenFromFriendFinder = true,
                        IsUsingRid = true,
                    }
                },
                Tags = new AccountFieldTags
                {
                    PrivacyInfoTag = 0xD7CA834D
                }
            });
        }

        //[BnetMethod(MethodId = 31)]
        public static void HandleGetGameAccountStateRequest(GetGameAccountStateRequest getGameAccountStateRequest, BnetSession session)
        {
            // TODO
        }
    }
}

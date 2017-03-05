// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using Bgs.Protocol.Account.V1;
using BnetServer.Attributes;
using BnetServer.Constants.Service;
using BnetServer.Network;

namespace BnetServer.Packets.Services
{
    [BnetService(Hash = BnetServiceHash.AccountService)]
    class AccountService
    {
        [BnetServiceMethod(MethodId = 30)]
        public static Task HandleGetAccountStateRequest(GetAccountStateRequest getAccountStateRequest, BnetServiceSession session)
        {
            return session.Send(new GetAccountStateResponse
            {
                // TODO: Implement?!
                State = new AccountState
                {
                    PrivacyInfo = new PrivacyInfo
                    {
                        IsHiddenFromFriendFinder = true,
                        IsUsingRid = true,
                    }
                },
                // TODO: Implement?!
                Tags = new AccountFieldTags
                {
                    PrivacyInfoTag = 0xD7CA834D
                }
            });
        }

        [BnetServiceMethod(MethodId = 31)]
        public static async void HandleGetGameAccountStateRequest(GetGameAccountStateRequest getGameAccountStateRequest, BnetServiceSession session)
        {
            var gameAccount = session.Account.GameAccounts.SingleOrDefault(ga => ga.Id == getGameAccountStateRequest.GameAccountId.Low);

            if (gameAccount != null)
            {
                var getGameAccountStateResponse = new GetGameAccountStateResponse
                {
                    State = new GameAccountState
                    {
                        GameLevelInfo = new GameLevelInfo
                        {
                            // 'WoW'
                            Program = 0x576F57,
                            Name = gameAccount.Game + gameAccount.Index
                        },
                        GameStatus = new GameStatus
                        {
                            // 'WoW'
                            Program = 0x576F57,
                        }
                    },
                    // TODO: Implement?!
                    Tags = new GameAccountFieldTags
                    {
                        GameLevelInfoTag = 4140539163,
                        GameStatusTag = 2562154393
                    }
                };

                await session.Send(getGameAccountStateResponse);
            }
        }
    }
}

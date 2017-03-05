// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Bgs.Protocol;
using Bgs.Protocol.Authentication.V1;
using Bgs.Protocol.Challenge.V1;
using BnetServer.Attributes;
using BnetServer.Constants.Service;
using BnetServer.Misc;
using BnetServer.Network;
using Framework.Misc;
using Google.Protobuf;

namespace BnetServer.Packets.Services
{
    [BnetService(Hash = BnetServiceHash.AuthenticationServerService)]
    public class AuthenticationServerService
    {
        [BnetServiceMethod(MethodId = 1)]
        public static async void HandleLogonRequest(LogonRequest logonRequest, BnetServiceSession session)
        {
            // TODO: Implement version checks, etc.
            //if (DB.Auth.Any<Application>(a => a.Program == logonRequest.Program))
            {
                var challengeExternalRequest = new ChallengeExternalRequest
                {
                    PayloadType = "web_auth_url",
                    Payload = ByteString.CopyFromUtf8($"https://{BnetConfig.RestServiceHost}:{BnetConfig.RestServiceBindPort}/login/{session.Guid}")
                };

                await session.Send(challengeExternalRequest, BnetServiceHash.AuthenticationClientService, 3);
            }
        }

        [BnetServiceMethod(MethodId = 7)]
        public static async void HandleVerifyWebCredentialsRequest(VerifyWebCredentialsRequest verifyWebCredentials, BnetServiceSession session)
        {
            var logonResult = new LogonResult();

            if (verifyWebCredentials.WebCredentials.ToStringUtf8() == session.LoginTicket)
            {
                logonResult.AccountId = new EntityId
                {
                    High = 0x100000000000000,
                    Low = session.Account.Id
                };

                session.Account.GameAccounts.ForEach(ga =>
                {
                    logonResult.GameAccountId.Add(new EntityId
                    {
                        // TODO: Build the right High value.
                        High = 0x200000200576F57,
                        Low = ga.Id
                    });
                });

                logonResult.SessionKey = ByteString.CopyFromUtf8(new byte[0].GenerateRandomKey(64).ToHexString());
            }
            else
                logonResult.ErrorCode = (uint)BnetServiceErrorCode.Denied;

            await session.Send(logonResult, BnetServiceHash.AuthenticationListenerService, 5);
        }
    }
}

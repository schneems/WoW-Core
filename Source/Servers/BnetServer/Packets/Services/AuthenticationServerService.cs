// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Bgs.Protocol.Authentication.V1;
using Bgs.Protocol.Challenge.V1;
using BnetServer.Attributes;
using BnetServer.Constants.Bnet;
using BnetServer.Misc;
using BnetServer.Network;
using Google.Protobuf;

namespace BnetServer.Packets.Services
{
    [BnetService(Hash = BnetServiceHash.AuthenticationServerService)]
    public class AuthenticationServerService
    {
        //[BnetMethod(MethodId = 1)]
        public static async void HandleLogonRequest(LogonRequest logonRequest, BnetSession session)
        {
            // TODO: Implement version checks, etc.
            //if (DB.Auth.Any<Application>(a => a.Program == logonRequest.Program))
            {
                var challengeExternalRequest = new ChallengeExternalRequest
                {
                    PayloadType = "web_auth_url",
                    Payload = ByteString.CopyFromUtf8($"https://{BnetConfig.BnetChallengeHost}:{BnetConfig.BnetChallengeBindPort}/login/{session.Guid}")
                };

                await session.Send(challengeExternalRequest, BnetServiceHash.AuthenticationClientService, 3);
            }
        }

        //[BnetMethod(MethodId = 7)]
        public static async void HandleVerifyWebCredentialsRequest(VerifyWebCredentialsRequest verifyWebCredentials, BnetSession session)
        {
            var logonResult = new LogonResult();

            // TODO: Implement LogonResult.

            await session.Send(logonResult, BnetServiceHash.AuthenticationListenerService, 5);
        }
    }
}

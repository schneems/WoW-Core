// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BnetServer.Attributes;
using BnetServer.Network;
using Framework.Web;

namespace BnetServer.Packets.Rest
{
    [RestService(Host = "*")]
    class RestHandler
    {
        [RestRoute(Method = "GET", Path = "login")]
        public static async void HandleConnectRequest(HttpHeader request, BnetChallengeSession session)
        {
            // Login form is the same for all clients...
            await session.Send(RestResponse.LoginForm);
        }

        //[RestRoute(Method = "POST", Path = "login")]
        public static async void HandleLoginRequest(HttpHeader request, BnetChallengeSession session)
        {
            // TODO: Implement authentication.
            if (false)
            {
            }
            else
                await session.Send(RestResponse.InvalidAccountOrCredentials);
        }
    }
}

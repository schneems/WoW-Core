// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AuthServer.Attributes;
using AuthServer.Constants.Authentication;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using AuthServer.Network.Sessions;
using AuthServer.Packets.Client.Misc;
using AuthServer.Packets.Client.Net;
using AuthServer.Packets.Server.Net;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;

namespace AuthServer.Packets.Handlers
{
    class MiscHandler
    {
        [AuthMessage(AuthClientMessage.InformationRequest, AuthChannel.Authentication)]
        public static async void HandleInformationRequest(InformationRequest informationRequest, AuthSession session)
        {
            informationRequest.Components.
                FindAll(co => !DB.Auth.Any<Component>(c => c.Program == co.Program && c.Platform == co.Platform && c.Build == co.Version)).
                ForEach(async co =>
                {
                    if (!DB.Auth.Any<Component>(c => c.Program == co.Program))
                    {
                        await AuthHandler.SendAuthComplete(true, AuthResult.InvalidProgram, session);
                        return;
                    }

                    if (!DB.Auth.Any<Component>(c => c.Platform == co.Platform))
                    {
                        await AuthHandler.SendAuthComplete(true, AuthResult.InvalidPlatform, session);
                        return;
                    }

                    if (!DB.Auth.Any<Component>(c => c.Build == co.Version))
                    {
                        await AuthHandler.SendAuthComplete(true, AuthResult.InvalidGameVersion, session);
                        return;
                    }
                });

            var account = DB.Auth.Single<Account>(a => a.Email == informationRequest.AccountName);

            // First account lookup on database
            if ((session.Account = account) != null)
            {
                session.Program  = informationRequest.Program;
                session.Platform = informationRequest.Platform;

                await AuthHandler.SendProofRequest(session);
            }
            else
                await AuthHandler.SendAuthComplete(true, AuthResult.BadLoginInformation, session);
        }

        [AuthMessage(AuthClientMessage.Ping, AuthChannel.Connection)]
        public static async void HandlePing(Ping ping, AuthSession session)
        {
            await session.Send(new Pong());
        }

        [AuthMessage(AuthClientMessage.Disconnect, AuthChannel.Connection)]
        public static void HandleDisconnect(Disconnect disconnect, AuthSession session)
        {
            Log.Debug($"Client '{session.ConnectionInfo}' disconnected.");

            session.Dispose();
        }

        [AuthMessage(AuthClientMessage.Receive, (AuthChannel)5)]
        public static async void HandleHTTPReceive(HTTPReceive httpReceive, AuthSession session)
        {
            await Manager.PatchMgr.Send(httpReceive.Path, session);
        }
    }
}

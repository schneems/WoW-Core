// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography;
using System.Text;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Net;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Database.Character.Entities;
using Framework.Misc;
using Framework.Packets.Client.Authentication;
using Framework.Packets.Server.Net;
using World.Shared.Game.Entities;
using WorldNode.Managers;
using WorldNode.Network;

namespace WorldNode.Packets.Handlers
{
    class AuthHandler
    {
        [GlobalMessage(GlobalClientMessage.AuthContinuedSession, SessionState.Initiated)]
        public static async void HandleAuthContinuedSession(AuthContinuedSession authContinuedSession, NodeSession session)
        {
            var accountInfo = Manager.Redirect.GetAccountInfo(authContinuedSession.Key);

            // Delete redirect key
            Manager.Redirect.DeleteRedirectKey(authContinuedSession.Key);

            if (accountInfo != null)
            {
                var sha1 = new SHA1Managed();

                var emailBytes = Encoding.UTF8.GetBytes(accountInfo.Item1.Id + "#" + accountInfo.Item2.Index);
                var sessionKeyBytes = accountInfo.Item2.SessionKey.ToByteArray();
                var challengeBytes = BitConverter.GetBytes(session.Challenge);

                sha1.TransformBlock(emailBytes, 0, emailBytes.Length, emailBytes, 0);
                sha1.TransformBlock(sessionKeyBytes, 0, 40, sessionKeyBytes, 0);
                sha1.TransformFinalBlock(challengeBytes, 0, 4);

                if (sha1.Hash.Compare(authContinuedSession.Digest))
                {
                    session.State = SessionState.Authenticated;

                    session.Account = DB.Auth.Single<Account>(a => a.Id == accountInfo.Item2.AccountId);
                    session.GameAccount = accountInfo.Item2;
                    session.Player = new Player(DB.Character.Single<Character>(c => c.Guid == accountInfo.Item3), false);

                    session.Crypt = new Framework.Cryptography.WoW.WoWCrypt();
                    session.Crypt.Initialize(accountInfo.Item2.SessionKey.ToByteArray(), session.ClientSeed, session.ServerSeed);

                    // Resume on the new connection
                    await session.Send(new ResumeComms());

                    session.State = SessionState.InWorld;

                    return;
                }
            }

            session.Dispose();
        }
    }
}

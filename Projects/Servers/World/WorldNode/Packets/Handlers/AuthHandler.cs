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
using Framework.Misc;
using Framework.Packets.Client.Authentication;
using Framework.Packets.Server.Net;
using WorldNode.Managers;
using WorldNode.Network;

namespace WorldNode.Packets.Handlers
{
    class AuthHandler
    {
        [GlobalMessage(GlobalClientMessage.AuthContinuedSession, SessionState.Initiated)]
        public static void HandleAuthContinuedSession(AuthContinuedSession authContinuedSession, NodeSession session)
        {
            var accountInfo = Manager.Redirect.GetAccountInfo(authContinuedSession.Key);

            // Delete redirect key
            Manager.Redirect.DeleteCharacterRedirect(authContinuedSession.Key);

            if (accountInfo != null)
            {
                var sha1 = new SHA1Managed();

                var emailBytes = Encoding.UTF8.GetBytes(accountInfo.Item1.AccountId + "#" + accountInfo.Item1.Index);
                var sessionKeyBytes = accountInfo.Item1.SessionKey.ToByteArray();
                var challengeBytes = BitConverter.GetBytes(session.Challenge);

                sha1.TransformBlock(emailBytes, 0, emailBytes.Length, emailBytes, 0);
                sha1.TransformBlock(sessionKeyBytes, 0, 40, sessionKeyBytes, 0);
                sha1.TransformFinalBlock(challengeBytes, 0, 4);

                if (sha1.Hash.Compare(authContinuedSession.Digest))
                {
                    session.State = SessionState.Authenticated;

                    session.Account = DB.Auth.Single<Account>(a => a.Id == accountInfo.Item1.AccountId);
                    session.GameAccount = accountInfo.Item1;

                    session.Crypt = new Framework.Cryptography.WoW.WoWCrypt();
                    session.Crypt.Initialize(accountInfo.Item1.SessionKey.ToByteArray(), session.ClientSeed, session.ServerSeed);

                    // Resume on the new connection
                    session.Send(new ResumeComms());

                    return;
                }
            }

            session.Dispose();
        }
    }
}

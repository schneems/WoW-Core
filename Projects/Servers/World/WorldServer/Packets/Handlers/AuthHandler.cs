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

using System;
using System.Security.Cryptography;
using System.Text;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Net;
using Framework.Misc;
using Framework.Packets.Client.Authentication;
using Framework.Packets.Server.Net;
using WorldServer.Managers;
using WorldServer.Network;

namespace WorldServer.Packets.Handlers
{
    class AuthHandler
    {
        // Only GameAccount redirects supported for now.
        [GlobalMessage(GlobalClientMessage.AuthContinuedSession, SessionState.Initiated)]
        public static void HandleAuthContinuedSession(AuthContinuedSession authContinuedSession, WorldSession session)
        {
            var accountInfo = Manager.Redirect.GetAccountInfo(authContinuedSession.Key);

            // Delete redirect key
            Manager.Redirect.DeleteGameAccountRedirect(authContinuedSession.Key);

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

                    session.Account = accountInfo.Item1;
                    session.GameAccount = accountInfo.Item2;

                    session.Crypt = new Framework.Cryptography.WoW.WoWCrypt();
                    session.Crypt.Initialize(accountInfo.Item2.SessionKey.ToByteArray(), session.ClientSeed, session.ServerSeed);

                    // Resume on the new connection
                    session.Send(new ResumeComms());

                    return;
                }
            }

            session.Dispose();
        }
    }
}

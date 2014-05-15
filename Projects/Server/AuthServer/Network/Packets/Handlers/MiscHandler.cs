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

using System.Linq;
using AuthServer.Attributes;
using AuthServer.Constants.Authentication;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using Framework.Constants.Misc;
using Framework.Database;
using Framework.Logging;
using Framework.Network.Packets;

namespace AuthServer.Network.Packets.Handlers
{
    class MiscHandler
    {
        [AuthMessage(AuthClientMessage.InformationRequest, AuthChannel.BattleNet)]
        public static void OnInformationRequest(AuthPacket packet, AuthSession session)
        {
            if (!Manager.GetState())
            {
                AuthHandler.SendAuthComplete(true, AuthResult.ServerBusy, session);
                return;
            }

            Log.Message(LogType.Debug, "Program: {0}", packet.ReadFourCC());

            var os = packet.ReadFourCC();
            var language = packet.ReadFourCC();

            Log.Message(LogType.Debug, "Platform: {0}", os);
            Log.Message(LogType.Debug, "Locale: {0}", language);

            var componentCount = packet.Read<int>(6);

            for (int i = 0; i < componentCount; i++)
            {
                var program = packet.ReadFourCC();
                var platform = packet.ReadFourCC();
                var build = packet.Read<int>(32);

                Log.Message(LogType.Debug, "Program: {0}", program);
                Log.Message(LogType.Debug, "Platform: {0}", platform);
                Log.Message(LogType.Debug, "Locale: {0}", build);

                if (DB.Auth.Components.Any(c => c.Program == program && c.Platform == platform && c.Build == build))
                    continue;

                if (!DB.Auth.Components.Any(c => c.Program == program))
                {
                    AuthHandler.SendAuthComplete(true, AuthResult.InvalidProgram, session);
                    return;
                }

                if (!DB.Auth.Components.Any(c => c.Platform == platform))
                {
                    AuthHandler.SendAuthComplete(true, AuthResult.InvalidPlatform, session);
                    return;
                }

                if (!DB.Auth.Components.Any(c => c.Build == build))
                {
                    AuthHandler.SendAuthComplete(true, AuthResult.InvalidGameVersion, session);
                    return;
                }
            }

            var hasAccountName = packet.Read<bool>(1);

            if (hasAccountName)
            {
                var accountLength = packet.Read<int>(9) + 3;
                var accountName = packet.ReadString(accountLength);
                var account = DB.Auth.Accounts.SingleOrDefault(a => a.Email == accountName);

                // First account lookup on database
                if ((session.Account = account) != null)
                {
                    session.Account.IP = session.GetClientIP();
                    session.Account.OS = os;
                    session.Account.Language = language;

                    DB.Auth.Update(session.Account, "IP", session.Account.IP, "OS", session.Account.OS, "Language", session.Account.Language);

                    AuthHandler.SendProofRequest(session);
                }
                else
                    AuthHandler.SendAuthComplete(true, AuthResult.BadLoginInformation, session);
            }
        }

        [AuthMessage(AuthClientMessage.Ping, AuthChannel.Creep)]
        public static void OnPing(AuthPacket packet, AuthSession session)
        {
            var pong = new AuthPacket(AuthServerMessage.Pong, AuthChannel.Creep);

            session.Send(pong);
        }
    }
}

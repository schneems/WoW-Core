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

using CharacterServer.Attributes;
using CharacterServer.Constants.Net;
using Framework.Network.Packets;

namespace CharacterServer.Network.Packets.Handlers
{
    class CharacterHandler
    {
        [Message(ClientMessages.EnumCharacters)]
        public static void OnEnumCharacters(Packet packet, CharacterSession session)
        {

        }

        public static void HandleEnumCharactersResult(CharacterSession session)
        {

        }

        [Message(ClientMessages.CreateCharacter)]
        public static void OnCreateCharacter(Packet packet, CharacterSession session)
        {

        }

        public static void HandleCreateChar(CharacterSession session)
        {

        }

        [Message(ClientMessages.CharDelete)]
        public static void OnCharDelete(Packet packet, CharacterSession session)
        {

        }

        public static void HandleDeleteChar(CharacterSession session)
        {

        }

        [Message(ClientMessages.GenerateRandomCharacterName)]
        public static void OnGenerateRandomCharacterName(Packet packet, CharacterSession session)
        {

        }

        public static void HandleGenerateRandomCharacterNameResult(CharacterSession session)
        {

        }
    }
}

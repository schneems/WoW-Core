/*
 * Copyright (C) 2012-2014 Arctium <http://arctium.org>
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

using Framework.Console.Commands;
using Framework.Constants;
using Framework.Database;
using Framework.ObjectDefines;
using WorldServer.Game.Packets.PacketHandler;
using WorldServer.Network;

namespace WorldServer.Game.Chat.Commands
{
    public class PlayerCommands : Globals
    {
        [ChatCommand("setlevel")]
        public static void AddNpc(string[] args, WorldClass session)
        {
            var level = CommandParser.Read<byte>(args, 1);
            var chatMessage = new ChatMessageValues(0, "Please enter a level between 0 and 91.");

            if (level > 90 || level < 1)
            {
                ChatHandler.SendMessage(ref session, chatMessage);
                return;
            }

            var pChar = session.Character;

            if (SmartGuid.GetGuidType(pChar.TargetGuid) == HighGuidType.Player)
            {
               if (DB.Characters.Execute("UPDATE Characters SET Level = ? WHERE Guid = ?", level, pChar.Guid))
               {
                   pChar.SetUpdateField<uint>((int)UnitFields.Level, level);
                   
                   ObjectHandler.HandleUpdateObjectValues(ref session);
               }
            }
            else
            {
                chatMessage.Message = "Please select a player.";

                ChatHandler.SendMessage(ref session, chatMessage);
            }
        }
    }
}

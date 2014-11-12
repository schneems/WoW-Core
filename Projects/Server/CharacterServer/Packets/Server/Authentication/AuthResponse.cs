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

using CharacterServer.Constants.Authentication;
using CharacterServer.Constants.Net;
using CharacterServer.Packets.Structures.Authentication;
using Framework.Network.Packets;

namespace CharacterServer.Packets.Server.Authentication
{
    public class AuthResponse : ServerPacket
    {
        public AuthResult Result           { get; set; }
        public bool HasSuccessInfo         { get; set; }
        public bool HasWaitInfo            { get; set; }
        public AuthSuccessInfo SuccessInfo { get; set; } = new AuthSuccessInfo();
        public AuthWaitInfo WaitInfo       { get; set; } = new AuthWaitInfo();

        public AuthResponse() : base(ServerMessage.AuthResponse) { }

        public override void Write()
        {
            Packet.Write((byte)Result);

            Packet.PutBit(HasSuccessInfo);
            Packet.PutBit(HasWaitInfo);
            Packet.Flush();

            if (HasSuccessInfo)
                SuccessInfo.Write(Packet);

            if (HasWaitInfo)
                WaitInfo.Write(Packet);
        }
    }
}

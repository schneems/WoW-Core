/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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
using Framework.Network.Packets;

namespace WorldServer.Packets.Structures.Movement
{
    class MovementUpdate : IServerStruct
    {
        public float WalkSpeed       { get; set; } = 2.5f;
        public float RunSpeed        { get; set; } = 7f;
        public float RunBackSpeed    { get; set; } = 2.5f;
        public float SwimSpeed       { get; set; } = 4.72222f;
        public float SwimBackSpeed   { get; set; } = 4.5f;
        public float FlightSpeed     { get; set; } = 7f;
        public float FlightBackSpeed { get; set; } = 4.5f;
        public float TurnRate        { get; set; } = (float)Math.PI;
        public float PitchRate       { get; set; } = (float)Math.PI;
        public MovementStatus Status { get; } = new MovementStatus();

        public void Write(Packet packet)
        {
            Status.Write(packet);

            packet.Write(WalkSpeed);
            packet.Write(RunSpeed);
            packet.Write(RunBackSpeed);
            packet.Write(SwimSpeed);
            packet.Write(SwimBackSpeed);
            packet.Write(FlightSpeed);
            packet.Write(FlightBackSpeed);
            packet.Write(TurnRate);
            packet.Write(PitchRate);

            packet.Write(0);

            packet.PutBit(false);
            packet.FlushBits();
        }
    }
}

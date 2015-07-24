// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

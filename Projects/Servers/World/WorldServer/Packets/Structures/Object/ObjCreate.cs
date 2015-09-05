// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Network.Packets;
using WorldServer.Packets.Structures.Movement;

namespace WorldServer.Packets.Structures.Object
{
    class ObjCreate : IServerStruct
    {
        public bool NoBirthAnim    { get; set; }
        public MovementUpdate Move { get; set; }
        public Position Stationary { get; set; }
        public bool HasRotation    { get; set; }
        public long Rotation       { get; set; }
        public bool ThisIsYou      { get; set; }

        public void Write(Packet packet)
        {
            packet.PutBit(NoBirthAnim);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(Move != null);
            packet.PutBit(false);
            packet.PutBit(Stationary != null);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(HasRotation);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(ThisIsYou);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.PutBit(false);
            packet.FlushBits();

            packet.Write(0);

            if (Move != null)
                Move.Write(packet);

            if (Stationary != null)
                Stationary.Write(packet);

            if (HasRotation)
                packet.Write(Rotation);
        }
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Net;
using Framework.Network.Packets;

namespace Framework.Packets.Server.Net
{
    public class ResumeComms : ServerPacket
    {
        public ResumeComms() : base(GlobalServerMessage.ResumeComms) { }

        public override void Write()
        {
        }
    }
}

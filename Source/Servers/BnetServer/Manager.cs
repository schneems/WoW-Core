// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BnetServer.Managers;
using BnetServer.Packets;

public class Manager
{
    public static PacketManager Packet => PacketManager.Instance;
    public static SessionManager Session => SessionManager.Instance;
}

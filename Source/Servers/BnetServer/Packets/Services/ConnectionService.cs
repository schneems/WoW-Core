// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Bgs.Protocol;
using Bgs.Protocol.Connection.V1;
using BnetServer.Attributes;
using BnetServer.Constants.Service;
using BnetServer.Network;
using Framework.Logging;

namespace BnetServer.Packets.Services
{
    [BnetService(Hash = BnetServiceHash.ConnectionService)]
    public class ConnectionService
    {
        [BnetServiceMethod(MethodId = 1)]
        public static async void HandleConnectRequest(ConnectRequest connectRequest, BnetServiceSession session)
        {
            // TODO: Verify sent values.
            await session.Send(new ConnectResponse
            {
                ClientId = connectRequest.ClientId,
                UseBindlessRpc = connectRequest.UseBindlessRpc,

                ServerTime = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                ServerId = new ProcessId
                {
                    Epoch = 0,
                    Label = 0
                }
            });
        }

        [BnetServiceMethod(MethodId = 5)]
        public static async void HandlePing(NoData noData, BnetServiceSession session)
        {
            await session.Send(new NoData(), BnetServiceHash.ConnectionService, 5);
        }

        [BnetServiceMethod(MethodId = 7)]
        public static async void HandleDisconnectRequest(DisconnectRequest disconnectRequest, BnetServiceSession session)
        {
            Log.Message(LogTypes.Info, $"Client '{session.GetClientInfo()} disconnected ({disconnectRequest.ErrorCode}).");

            await session.Send(new DisconnectNotification
            {
                ErrorCode = disconnectRequest.ErrorCode,
            }, BnetServiceHash.ConnectionService, 4);
        }
    }
}

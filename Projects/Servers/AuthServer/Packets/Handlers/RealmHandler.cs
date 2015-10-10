// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using AuthServer.Attributes;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using AuthServer.Network.Sessions;
using AuthServer.Packets.Client.Realm;
using AuthServer.Packets.Server.Realm;
using Framework.Misc;

namespace AuthServer.Packets.Handlers
{
    class RealmHandler
    {
        [AuthMessage(AuthClientMessage.JoinRequest, AuthChannel.WoWRealm)]
        public static async void HandleJoinRequest(JoinRequest joinRequest, AuthSession session)
        {
            var serverSalt = new byte[0].GenerateRandomKey(4);

            session.GenerateSessionKey(BitConverter.GetBytes(joinRequest.ClientSalt), serverSalt);

            // Continue if sessionKey is not empty
            if (session.GameAccount.SessionKey != "")
            {
                var joinResponse = new JoinResponse
                {
                    RealmCount = Manager.RealmMgr.RealmList.Count,
                    ServerSalt = BitConverter.ToUInt32(serverSalt, 0)
                };

                foreach (var realm in Manager.RealmMgr.RealmList)
                {
                    var cRealm = Manager.RealmMgr.GetRealm(realm.Value.Id);

                    if (cRealm == null)
                    {
                        session.Dispose();
                        break;
                    }

                    var ip = IPAddress.Parse(cRealm.IPAddress).GetAddressBytes();
                    var port = BitConverter.GetBytes(cRealm.Port);

                    Array.Reverse(port);

                    joinResponse.RealmInfo.Add(Tuple.Create(ip, port));
                }

                await session.Send(joinResponse);
            }
        }

        [AuthMessage(AuthClientMessage.ListSubscribeRequest, AuthChannel.WoWRealm)]
        public static async void HandleListSubscribeRequest(ListSubscribeRequest listSubscribeRequest, AuthSession session)
        {
            var listSubscribeResponse = new ListSubscribeResponse();

            var index = 0;

            // Battlenet::Client::WoWRealm::ListUpdate
            foreach (var realm in Manager.RealmMgr.RealmList)
            {
                listSubscribeResponse.ListUpdates.Add(new ListUpdate
                {
                    Index = ++index,
                    RealmInfo = realm.Value
                });
            }

            await session.Send(listSubscribeResponse);
        }
    }
}

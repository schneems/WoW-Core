// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using AuthServer.Attributes;
using AuthServer.Constants.Net;
using AuthServer.Managers;
using AuthServer.Network.Sessions;
using Framework.Constants.Misc;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;

namespace AuthServer.Network.Packets.Handlers
{
    class RealmHandler
    {
        [AuthMessage(AuthClientMessage.JoinRequest, AuthChannel.WoWRealm)]
        public static void OnJoinRequest(AuthPacket packet, Client client)
        {
            var clientSalt = BitConverter.GetBytes(packet.Read<uint>(32));
            var serverSalt = new byte[0].GenerateRandomKey(4);

            client.Session.GenerateSessionKey(clientSalt, serverSalt);

            // Continue if sessionKey is not empty
            if (client.Session.GameAccount.SessionKey != "")
            {
                var joinResponse = new AuthPacket(AuthServerMessage.JoinResponse, AuthChannel.WoWRealm);

                joinResponse.Write(Manager.RealmMgr.RealmList.Count == 0, 1);

                joinResponse.Write(BitConverter.ToUInt32(serverSalt, 0), 32);

                // Battlenet::WoW::IP4AddressList
                joinResponse.Write(Manager.RealmMgr.RealmList.Count, 5);

                foreach (var realm in Manager.RealmMgr.RealmList)
                {
                    var ip = IPAddress.Parse(realm.Value.IP).GetAddressBytes();
                    var port = BitConverter.GetBytes(realm.Value.Port);

                    Array.Reverse(port);

                    joinResponse.Write(ip);
                    joinResponse.Write(port);
                }

                // Battlenet::WoW::IP6AddressList, not implemented
                joinResponse.Write(0, 5); 

                client.SendPacket(joinResponse);
            }
        }

        [AuthMessage(AuthClientMessage.ListSubscribeRequest, AuthChannel.WoWRealm)]
        public static void OnListSubscribeRequest(AuthPacket packet, Client client)
        {
            Log.Debug("Received ListSubscribeRequest.");

            // Battlenet::Client::WoWRealm::ListSubscribeResponse
            var listSubscribeResponse = new AuthPacket(AuthServerMessage.ListSubscribeResponse, AuthChannel.WoWRealm);

            listSubscribeResponse.Write(0, 1);
            listSubscribeResponse.Write(0, 7);

            var realmCounter = 0;

            // Battlenet::Client::WoWRealm::ListUpdate
            foreach (var realm in Manager.RealmMgr.RealmList)
            {
                var listUpdate = new AuthPacket(AuthServerMessage.ListUpdate, AuthChannel.WoWRealm);

                listUpdate.Write(true, 1);
                listUpdate.Write(realm.Value.Category, 32);          // RealmCategory
                listUpdate.Write(0, 32);                             // RealmPopulation, float written as uint32
                listUpdate.Write(realm.Value.State, 8);              // RealmState
                listUpdate.Write(realm.Value.Id, 19);                // RealmId
                listUpdate.Write(0x80000000 + realm.Value.Type, 32); // RealmType
                listUpdate.WriteString(realm.Value.Name, 10, false); // RealmName
                listUpdate.Write(false, 1);                          // Battlenet::VersionString, not used for now
                listUpdate.Write(realm.Value.Flags, 8);              // RealmInfoFlags
                listUpdate.Write(0, 8);
                listUpdate.Write(0, 12);
                listUpdate.Write(0, 8);
                listUpdate.Write(++realmCounter, 32);

                listUpdate.Finish();

                // Write ListUpdate data to ListSubscribeResponse
                listSubscribeResponse.Write(listUpdate.Data);
            }

            // Battlenet::Client::WoWRealm::ListComplete
            var listComplete = new AuthPacket(AuthServerMessage.ListComplete, AuthChannel.WoWRealm);

            listComplete.Finish();

            // Write ListComplete data to ListSubscribeResponse end
            listSubscribeResponse.Write(listComplete.Data);

            client.SendPacket(listSubscribeResponse);
        }
    }
}

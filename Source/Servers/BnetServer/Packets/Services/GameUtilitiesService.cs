// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bgs.Protocol;
using Bgs.Protocol.GameUtilities.V1;
using BnetServer.Attributes;
using BnetServer.Constants.Bnet;
using BnetServer.Misc;
using BnetServer.Network;
using Framework.Logging;
using Framework.Misc;
using Framework.Packets.Json.Realmlist;
using Google.Protobuf;
using static Framework.Serialization.Json;

namespace BnetServer.Packets.Services
{
    [BnetService(Hash = BnetServiceHash.GameUtilitiesService)]
    class GameUtilitiesService
    {
        static readonly Dictionary<string, Func<ClientRequest, BnetSession, Task>> clientRequestHandlers = new Dictionary<string, Func<ClientRequest, BnetSession, Task>>
        {
            //{ "Command_RealmListTicketRequest_v1_b9", HandleRealmListTicketRequest},
            //{ "Command_LastCharPlayedRequest_v1_b9", HandleLastCharPlayedRequest},
            //{ "Command_RealmListRequest_v1_b9", HandleRealmListRequest},
            //{ "Command_RealmJoinRequest_v1_b9", HandleRealmJoinRequest}
        };

        [BnetMethod(MethodId = 1)]
        public static async void HandleClientRequest(ClientRequest clientRequest, BnetSession session)
        {
            Func<ClientRequest, BnetSession, Task> clientRequestHandler;

            if (clientRequestHandlers.TryGetValue(clientRequest.Attribute[0].Name, out clientRequestHandler))
            {
                Log.Message(LogTypes.Error, $"client request '{clientRequest}'.\n");

                await clientRequestHandler(clientRequest, session);
            }
            else
                Log.Message(LogTypes.Error, $"Tried to call non existing handler for client request '{clientRequest.Attribute[0].Name}'.");
        }

        // TODO
        static async Task HandleRealmListTicketRequest(ClientRequest clientRequest, BnetSession session)
        {
            var paramIdentityValue = clientRequest.GetVariant("Param_Identity")?.BlobValue.ToStringUtf8();
            var paramClientInfoValue = clientRequest.GetVariant("Param_ClientInfo")?.BlobValue.ToStringUtf8();

            if (paramIdentityValue != null && paramClientInfoValue != null)
            {
                var realmListTicketIdentity = CreateObject<RealmListTicketIdentity>(paramIdentityValue, true);
                var realmListTicketClientInformation = CreateObject<RealmListTicketClientInformation>(paramClientInfoValue, true);

                // Dummy await.
                await HandleRealmListTicketRequest(clientRequest, session);
            }
            else
                session.Dispose();
        }

        // TODO
        static Task HandleLastCharPlayedRequest(ClientRequest clientRequest, BnetSession session)
        {
            var lastCharPlayedResponse = new ClientResponse();

            return session.Send(lastCharPlayedResponse);
        }

        // TODO
        static async Task HandleRealmListRequest(ClientRequest clientRequest, BnetSession session)
        {
            var realmJoinRequest = clientRequest.GetVariant("Command_RealmListRequest_v1_b9")?.StringValue;
            var realmListTicket = clientRequest.GetVariant("Param_RealmListTicket")?.BlobValue.ToByteArray();

            if (session.RealmListTicket.Compare(realmListTicket))
            {
                // TODO: Implement loading realms from database or realm service over IPC.
                var realmListResponse = new ClientResponse();
                var realmlist = new RealmListUpdates();

                realmListResponse.Attribute.Add(new Bgs.Protocol.Attribute
                {
                    Name = "Param_RealmList",
                    Value = new Variant
                    {
                        BlobValue = ByteString.CopyFrom(Deflate("JSONRealmListUpdates", realmlist))
                    }
                });

                // TODO: Implement existing character counts.
                var realmCharacterCountList = new RealmCharacterCountList();

                realmListResponse.Attribute.Add(new Bgs.Protocol.Attribute
                {
                    Name = "Param_CharacterCountList",
                    Value = new Variant
                    {
                        BlobValue = ByteString.CopyFrom(Deflate("JSONRealmCharacterCountList", realmCharacterCountList))
                    }
                });

                await session.Send(realmListResponse);
            }
        }

        // TODO
        static async Task HandleRealmJoinRequest(ClientRequest clientRequest, BnetSession session)
        {
            var realmJoinRequest = clientRequest.GetVariant("Command_RealmJoinRequest_v1_b9")?.StringValue;
            var realmAddress = clientRequest.GetVariant("Param_RealmAddress")?.UintValue;
            var realmListTicket = clientRequest.GetVariant("Param_RealmListTicket")?.BlobValue.ToByteArray();
            var bnetSessionKey = clientRequest.GetVariant("Param_BnetSessionKey")?.BlobValue.ToByteArray();

            // Check for valid realmlist ticket.
            if (realmListTicket.Compare(session.RealmListTicket))
            {
                var realmJoinResponse = new ClientResponse();

                await session.Send(realmJoinResponse);
            }
        }

        [BnetMethod(MethodId = 10)]
        public static async void HandleGetAllValuesForAttributeRequest(GetAllValuesForAttributeRequest getAllValuesForAttributeRequest, BnetSession session)
        {
            if (getAllValuesForAttributeRequest.AttributeKey == "Command_RealmListRequest_v1_b9")
            {
                var getAllValuesForAttributeResponse = new GetAllValuesForAttributeResponse();

                // TODO: Implement it the right way! 
                getAllValuesForAttributeResponse.AttributeValue.Add(new Variant { StringValue = "0-0-0" });

                await session.Send(getAllValuesForAttributeResponse);
            }
        }
    }
}

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

using System.Linq;
using CharacterServer.Attributes;
using CharacterServer.Constants.Character;
using CharacterServer.Constants.Net;
using CharacterServer.Managers;
using CharacterServer.ObjectStores;
using Framework.Constants.General;
using Framework.Constants.Object;
using Framework.Database;
using Framework.Database.Character.Entities;
using Framework.Misc;
using Framework.Network.Packets;
using Framework.Objects;

namespace CharacterServer.Network.Packets.Handlers
{
    class CharacterHandler
    {
        //[Message(ClientMessage.EnumCharacters)]
        public static void OnEnumCharacters(Packet packet, CharacterSession session)
        {
            HandleEnumCharactersResult(session);
        }

        public static void HandleEnumCharactersResult(CharacterSession session)
        {
            var gameAccount = session.GameAccount;
            var charList = DB.Character.Where<Character>(c => c.GameAccountId == gameAccount.Id);

            var enumCharactersResult = new Packet(ServerMessage.EnumCharactersResult);

            enumCharactersResult.PutBit(1);
            enumCharactersResult.PutBit(0);

            enumCharactersResult.Flush();

            enumCharactersResult.Write(charList.Count);
            enumCharactersResult.Write(0);

            foreach (var c in charList) 
            {
                var guid = new SmartGuid { Type = GuidType.Player, MapId = (ushort)c.Map, CreationBits = c.Guid };
                var guildGuid = new SmartGuid { Type = GuidType.Guild, CreationBits = c.GuildGuid };

                enumCharactersResult.Write(guid);
                enumCharactersResult.Write(c.ListPosition);
                enumCharactersResult.Write((byte)c.Race);
                enumCharactersResult.Write((byte)c.Class);
                enumCharactersResult.Write(c.Sex);
                enumCharactersResult.Write(c.Skin);
                enumCharactersResult.Write(c.Face);
                enumCharactersResult.Write(c.HairStyle);
                enumCharactersResult.Write(c.HairColor);
                enumCharactersResult.Write(c.FacialHairStyle);
                enumCharactersResult.Write(c.Level);
                enumCharactersResult.Write(c.Zone);
                enumCharactersResult.Write(c.Map);
                enumCharactersResult.Write(c.X);
                enumCharactersResult.Write(c.Y);
                enumCharactersResult.Write(c.Z);
                enumCharactersResult.Write(guildGuid);
                enumCharactersResult.Write((uint)c.CharacterFlags);
                enumCharactersResult.Write((uint)c.CustomizeFlags);
                enumCharactersResult.Write(c.Flags3);
                enumCharactersResult.Write(c.PetCreatureDisplayId);
                enumCharactersResult.Write(c.PetLevel);
                enumCharactersResult.Write(c.PetCreatureFamily);
                enumCharactersResult.Write(0);
                enumCharactersResult.Write(0);

                for (var i = 0; i < 23; i++)
                {
                    enumCharactersResult.Write(0);
                    enumCharactersResult.Write(0);
                    enumCharactersResult.Write((byte)0);
                }

                enumCharactersResult.PutBits(c.Name.Length, 6);
                enumCharactersResult.PutBit(1);
                enumCharactersResult.PutBit(0);

                enumCharactersResult.Write(c.Name);
            }

            //session.Send(enumCharactersResult);
        }

        //[Message(ClientMessage.CreateCharacter)]
        public static void OnCreateCharacter(Packet packet, CharacterSession session)
        {
            var nameLength     = packet.GetBits<int>(6);
            var useTemplateSet = packet.GetBit();

            var raceId            = packet.Read<Race>();
            var classId           = packet.Read<Class>();
            var sexId             = packet.Read<byte>();
            var skinId            = packet.Read<byte>();
            var faceId            = packet.Read<byte>();
            var hairStyleId       = packet.Read<byte>();
            var hairColorId       = packet.Read<byte>();
            var facialHairStyleId = packet.Read<byte>();

            packet.Skip(1);

            var name = packet.Read<string>(nameLength).ToLowerEnd();

            var createChar = new Packet(ServerMessage.CreateChar);

            if (!ClientDB.ChrRaces.Any(c => c.Id == raceId) || !ClientDB.ChrClasses.Any(c => c.Id == classId))
            {
                createChar.Write((byte)CharCreateCode.Failed);

                //session.Send(createChar);
                return;
            }

            if (!ClientDB.CharBaseInfo.Any(c => c.RaceId == raceId && c.ClassId == classId))
            {
                createChar.Write((byte)CharCreateCode.Failed);

                //session.Send(createChar);
                return;
            }

            if (DB.Character.Any<Character>(c => c.Name == name))
            {
                createChar.Write((byte)CharCreateCode.NameInUse);

                //session.Send(createChar);
                return;
            }

            if (useTemplateSet)
            {
                var templateSetId = packet.Read<int>();
                var accTemplate = session.GameAccount.GameAccountCharacterTemplates.Any(t => t.SetId == templateSetId);
                var realmTemplate = session.Realm.RealmCharacterTemplates.Any(t => t.SetId == templateSetId);

                if (accTemplate || realmTemplate)
                {
                    var template = DB.Character.Single<CharacterTemplateSet>(s => s.Id == templateSetId);
                    
                    // Implement...
                }
                else
                    createChar.Write((byte)CharCreateCode.Failed);
            }
            else
            {
                var creationData = DB.Character.Single<CharacterCreationData>(d => d.Race == raceId && d.Class == classId);
                var creationData2 = DB.Character.Select<CharacterCreationData>();

                if (creationData != null)
                {
                    var newChar = new Character
                    {
                        Name            = name,
                        GameAccountId   = session.GameAccount.Id,
                        RealmId         = session.Realm.Id,
                        Race            = raceId,
                        Class           = classId,
                        Sex             = sexId,
                        Skin            = skinId,
                        Face            = faceId,
                        HairStyle       = hairStyleId,
                        HairColor       = hairColorId,
                        FacialHairStyle = facialHairStyleId,
                        Level           = 1,
                        Map             = creationData.Map,
                        X               = creationData.X,
                        Y               = creationData.Y,
                        Z               = creationData.Z,
                        O               = creationData.O,
                        CharacterFlags  = CharacterFlags.Decline,
                        FirstLogin      = true
                    };

                    if (DB.Character.Add(newChar))
                    {
                        createChar.Write((byte)CharCreateCode.Success);

                        Manager.Character.LearnStartAbilities(newChar);
                    }
                    else
                        createChar.Write((byte)CharCreateCode.Failed);
                }
                else
                    createChar.Write((byte)CharCreateCode.Failed);
            }

            //session.Send(createChar);
        }

        //[Message(ClientMessage.CharDelete)]
        public static void OnCharDelete(Packet packet, CharacterSession session)
        {
            var guid = packet.Read<SmartGuid>();

            if (guid.CreationBits > 0 && guid.Type == GuidType.Player)
                HandleDeleteChar(session, guid);
        }

        public static void HandleDeleteChar(CharacterSession session, SmartGuid guid)
        {
            var gameAccount = session.GameAccount;
            var deleteChar = new Packet(ServerMessage.DeleteChar);

            if (DB.Character.Delete<Character>(c => c.Guid == guid.Low && c.GameAccountId == gameAccount.Id))
                deleteChar.Write((byte)CharDeleteCode.Success);
            else
                deleteChar.Write((byte)CharDeleteCode.Failed);

            //session.Send(deleteChar);
        }

        //[Message(ClientMessage.GenerateRandomCharacterName)]
        public static void OnGenerateRandomCharacterName(Packet packet, CharacterSession session)
        {

        }

        public static void HandleGenerateRandomCharacterNameResult(CharacterSession session)
        {

        }
    }
}

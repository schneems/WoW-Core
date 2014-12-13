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

using System;
using System.Linq;
using CharacterServer.Attributes;
using CharacterServer.Constants.Character;
using CharacterServer.Constants.Net;
using CharacterServer.Managers;
using CharacterServer.Network;
using CharacterServer.ObjectStores;
using CharacterServer.Packets.Client.Character;
using CharacterServer.Packets.Server.Character;
using CharacterServer.Packets.Structures.Character;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Character;
using Framework.Constants.Net;
using Framework.Constants.Object;
using Framework.Database;
using Framework.Database.Character.Entities;
using Framework.Objects;
using Framework.Packets.Client.Character;
using Framework.Packets.Server.Character;

namespace CharacterServer.Packets.Handlers
{
    class CharacterHandler
    {
        [Message(ClientMessage.EnumCharacters, SessionState.Authenticated)]
        public static void HandleEnumCharacters(EnumCharacters enumCharacters, CharacterSession session)
        {
            var charList = DB.Character.Where<Character>(c => c.GameAccountId == session.GameAccount.Id);

            var enumCharactersResult = new EnumCharactersResult();

            charList.ForEach(c =>
            {
                enumCharactersResult.Characters.Add(new CharacterListEntry
                {
                    Guid                 = new SmartGuid { Type = GuidType.Player, MapId = (ushort)c.Map, CreationBits = c.Guid },
                    Name                 = c.Name,
                    ListPosition         = c.ListPosition,
                    RaceID               = c.Race,
                    ClassID              = c.Class,
                    SexID                = c.Sex,
                    SkinID               = c.Skin,
                    FaceID               = c.Face,
                    HairStyle            = c.HairStyle,
                    HairColor            = c.HairColor,
                    FacialHairStyle      = c.FacialHairStyle,
                    ExperienceLevel      = c.Level,
                    ZoneID               = (int)c.Zone,
                    MapID                = (int)c.Map,
                    PreloadPos           = new Vector3 { X = c.X, Y = c.Y, Z = c.Z },
                    GuildGUID            = new SmartGuid { Type = GuidType.Guild, CreationBits = c.GuildGuid },
                    Flags                = c.CharacterFlags,
                    Flags2               = c.CustomizeFlags,
                    Flags3               = c.Flags3,
                    FirstLogin           = c.FirstLogin == 1,
                    PetCreatureDisplayID = c.PetCreatureDisplayId,
                    PetExperienceLevel   = c.PetLevel,
                    PetCreatureFamilyID  = c.PetCreatureFamily,
                });
            });

            session.Send(enumCharactersResult);
        }

        [Message(ClientMessage.CreateCharacter, SessionState.Authenticated)]
        public static void HandleCreateCharacter(CreateCharacter createCharacter, CharacterSession session)
        {
            var createChar = new CreateChar { Code = CharCreateCode.InProgress };

            if (!ClientDB.ChrRaces.Any(c => c.Id == createCharacter.RaceID) || !ClientDB.ChrClasses.Any(c => c.Id == createCharacter.ClassID))
                createChar.Code = CharCreateCode.Failed;
            else if (!ClientDB.CharBaseInfo.Any(c => c.RaceId == createCharacter.RaceID && c.ClassId == createCharacter.ClassID))
                createChar.Code = CharCreateCode.Failed;
            else if (DB.Character.Any<Character>(c => c.Name == createCharacter.Name))
                createChar.Code = CharCreateCode.NameInUse;
            else if (createChar.Code == CharCreateCode.InProgress)
            {
                if (createCharacter.TemplateSetID != 0)
                {
                    var accTemplate = session.GameAccount.GameAccountCharacterTemplates.Any(t => t.SetId == createCharacter.TemplateSetID);
                    var realmTemplate = session.Realm.RealmCharacterTemplates.Any(t => t.SetId == createCharacter.TemplateSetID);

                    if (accTemplate || realmTemplate)
                    {
                        var template = DB.Character.Single<CharacterTemplateSet>(s => s.Id == createCharacter.TemplateSetID);

                        // Not implemented = creation failed
                        createChar.Code = CharCreateCode.Failed;
                    }
                    else
                        createChar.Code = CharCreateCode.Failed;
                }
                else
                {
                    var creationData = DB.Character.Single<CharacterCreationData>(d => d.Race == createCharacter.RaceID && d.Class == createCharacter.ClassID);

                    if (creationData != null)
                    {
                        var newChar = new Character
                        {
                            Name            = createCharacter.Name,
                            GameAccountId   = session.GameAccount.Id,
                            RealmId         = session.Realm.Id,
                            Race            = createCharacter.RaceID,
                            Class           = createCharacter.ClassID,
                            Sex             = createCharacter.SexID,
                            Skin            = createCharacter.SkinID,
                            Face            = createCharacter.FaceID,
                            HairStyle       = createCharacter.HairStyleID,
                            HairColor       = createCharacter.HairColorID,
                            FacialHairStyle = createCharacter.FacialHairStyleID,
                            Level           = 1,
                            Map             = creationData.Map,
                            X               = creationData.X,
                            Y               = creationData.Y,
                            Z               = creationData.Z,
                            O               = creationData.O,
                            CharacterFlags  = CharacterFlags.Decline,
                            FirstLogin      = 1
                        };

                        if (DB.Character.Add(newChar))
                        {
                            createChar.Code = CharCreateCode.Success;

                            Manager.Character.LearnStartAbilities(newChar);
                        }
                        else
                            createChar.Code = CharCreateCode.Success;
                    }
                    else
                        createChar.Code = CharCreateCode.Failed;
                }
            }

            session.Send(createChar);
        }

        [Message(ClientMessage.CharDelete, SessionState.Authenticated)]
        public static void HandleCharDelete(CharDelete charDelete, CharacterSession session)
        {
            if (charDelete.Guid.CreationBits > 0 && charDelete.Guid.Type == GuidType.Player)
            {
                var deleteChar = new DeleteChar();
                var guid = charDelete.Guid;
                var gameAccount = session.GameAccount;

                if (DB.Character.Delete<Character>(c => c.Guid == guid.Low && c.GameAccountId == gameAccount.Id))
                    deleteChar.Code = CharDeleteCode.Success;
                else
                    deleteChar.Code = CharDeleteCode.Failed;

                session.Send(deleteChar);
            }
            else
                session.Dispose();
        }

        [Message(ClientMessage.GenerateRandomCharacterName, SessionState.Authenticated)]
        public static void HandleGenerateRandomCharacterName(GenerateRandomCharacterName generateRandomCharacterName, CharacterSession session)
        {
            var rand = new Random(Environment.TickCount);
            var generateRandomCharacterNameResult = new GenerateRandomCharacterNameResult();

            var names = ClientDB.NameGens.Where(n => n.RaceId == generateRandomCharacterName.Race && n.Sex == generateRandomCharacterName.Sex).Select(n => n.Name).ToList();

            do
            {
                generateRandomCharacterNameResult.Name = names[rand.Next(names.Count)];
            } while (DB.Character.Any<Character>(c => c.Name == generateRandomCharacterNameResult.Name));

            generateRandomCharacterNameResult.Success = generateRandomCharacterNameResult.Name != "";

            session.Send(generateRandomCharacterNameResult);
        }

        // Always send login failed here!
        [GlobalMessage(GlobalClientMessage.PlayerLogin, SessionState.Authenticated | SessionState.Redirected)]
        public static void HandlePlayerLogin(PlayerLogin playerLogin, CharacterSession session)
        {
            session.Send(new CharacterLoginFailed { Code = CharLoginCode.NoWorld });
        }
    }
}

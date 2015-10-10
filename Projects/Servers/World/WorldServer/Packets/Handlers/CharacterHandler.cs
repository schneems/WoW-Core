// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Framework.Attributes;
using Framework.Constants.Account;
using Framework.Constants.Net;
using Framework.Constants.Object;
using Framework.Database;
using Framework.Database.Character.Entities;
using Framework.Datastore;
using Framework.Logging;
using Framework.Objects;
using Framework.Packets.Handlers;
using World.Shared.Game.Entities;
using World.Shared.Game.Entities.Object.Guid;
using WorldServer.Attributes;
using WorldServer.Constants.Character;
using WorldServer.Constants.Net;
using WorldServer.Managers;
using WorldServer.Network;
using WorldServer.Packets.Client.Character;
using WorldServer.Packets.Server.Character;
using WorldServer.Packets.Server.Misc;
using WorldServer.Packets.Server.Spell;
using WorldServer.Packets.Structures.Character;

namespace WorldServer.Packets.Handlers
{
    class CharacterHandler
    {
        [Message(ClientMessage.EnumCharacters, SessionState.Authenticated)]
        public static async void HandleEnumCharacters(EnumCharacters enumCharacters, WorldSession session)
        {
            var charList = DB.Character.Where<Character>(c => c.GameAccountId == session.GameAccount.Id);

            var enumCharactersResult = new EnumCharactersResult();

            charList.ForEach(c =>
            {
                var character = new CharacterListEntry
                {
                    Guid = new PlayerGuid { CreationBits = c.Guid, RealmId = (ushort)c.RealmId },
                    Name = c.Name,
                    ListPosition = c.ListPosition,
                    RaceID = c.Race,
                    ClassID = c.Class,
                    SexID = c.Sex,
                    SkinID = c.Skin,
                    FaceID = c.Face,
                    HairStyle = c.HairStyle,
                    HairColor = c.HairColor,
                    FacialHairStyle = c.FacialHairStyle,
                    ExperienceLevel = c.ExperienceLevel,
                    ZoneID = (int)c.Zone,
                    MapID = (int)c.Map,
                    PreloadPos = new Vector3 { X = c.X, Y = c.Y, Z = c.Z },
                    GuildGUID = new GuildGuid { CreationBits = c.GuildGuid },
                    Flags = c.CharacterFlags,
                    Flags2 = c.CustomizeFlags,
                    Flags3 = c.Flags3,
                    FirstLogin = c.FirstLogin == 1,
                    PetCreatureDisplayID = c.PetCreatureDisplayId,
                    PetExperienceLevel = c.PetLevel,
                    PetCreatureFamilyID = c.PetCreatureFamily,
                };

                if (c.CharacterItems != null)
                {
                    for (var i = 0; i < character.InventoryItems.Length; i++)
                    {
                        foreach (var ci in c.CharacterItems)
                        {
                            Framework.Database.Data.Entities.Item item;

                            if ((int)ci.Slot == i && ClientDB.Items.TryGetValue(ci.ItemId, out item) && ci.Equipped)
                            {
                                character.InventoryItems[i].DisplayID = (uint)item.DisplayId;
                                character.InventoryItems[i].InvType = (byte)item.Slot;

                                break;
                            }

                        }
                    }
                }

                enumCharactersResult.Characters.Add(character);
            });

            await session.Send(enumCharactersResult);
        }

        [Message(ClientMessage.CreateCharacter, SessionState.Authenticated)]
        public static async void HandleCreateCharacter(CreateCharacter createCharacter, WorldSession session)
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
                            Name = createCharacter.Name,
                            GameAccountId = session.GameAccount.Id,
                            RealmId = session.Realm.Id,
                            Race = createCharacter.RaceID,
                            Class = createCharacter.ClassID,
                            Sex = createCharacter.SexID,
                            Skin = createCharacter.SkinID,
                            Face = createCharacter.FaceID,
                            HairStyle = createCharacter.HairStyleID,
                            HairColor = createCharacter.HairColorID,
                            FacialHairStyle = createCharacter.FacialHairStyleID,
                            Map = creationData.Map,
                            X = creationData.X,
                            Y = creationData.Y,
                            Z = creationData.Z,
                            O = creationData.O,
                            CharacterFlags = CharacterFlags.Decline,
                            FirstLogin = 1,
                            ExperienceLevel = 1
                        };

                        if (DB.Character.Add(newChar))
                        {
                            Manager.Character.LearnStartAbilities(newChar);
                            Manager.Character.AddStartItems(newChar);

                            createChar.Code = CharCreateCode.Success;
                        }
                        else
                            createChar.Code = CharCreateCode.Success;
                    }
                    else
                        createChar.Code = CharCreateCode.Failed;
                }
            }

            await session.Send(createChar);
        }

        [Message(ClientMessage.CharDelete, SessionState.Authenticated)]
        public static async void HandleCharDelete(CharDelete charDelete, WorldSession session)
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

                await session.Send(deleteChar);
            }
            else
                session.Dispose();
        }

        [Message(ClientMessage.GenerateRandomCharacterName, SessionState.Authenticated)]
        public static async void HandleGenerateRandomCharacterName(GenerateRandomCharacterName generateRandomCharacterName, WorldSession session)
        {
            var rand = new Random(Environment.TickCount);
            var generateRandomCharacterNameResult = new GenerateRandomCharacterNameResult();

            var names = ClientDB.NameGens.Where(n => n.RaceId == generateRandomCharacterName.Race && n.Sex == generateRandomCharacterName.Sex).Select(n => n.Name).ToList();

            do
            {
                generateRandomCharacterNameResult.Name = names[rand.Next(names.Count)];
            } while (DB.Character.Any<Character>(c => c.Name == generateRandomCharacterNameResult.Name));

            generateRandomCharacterNameResult.Success = generateRandomCharacterNameResult.Name != "";

            await session.Send(generateRandomCharacterNameResult);
        }

        [GlobalMessage(GlobalClientMessage.PlayerLogin, SessionState.Authenticated)]
        public static async void HandlePlayerLogin(PlayerLogin playerLogin, WorldSession session)
        {
            Log.Debug($"Character with GUID '{playerLogin.PlayerGUID.CreationBits}' tried to login...");

            var character = DB.Character.Single<Character>(c => c.Guid == playerLogin.PlayerGUID.CreationBits && c.GameAccountId == session.GameAccount.Id);

            if (character != null)
            {
                var worldNode = Manager.Redirect.GetWorldNode((int)character.Map);

                if (worldNode != null)
                {
                    // Create new player.
                    session.Player = new Player(character);

                    // Suspend the current connection & redirect
                    // Disable (causes disconnect).
                    //await session.Send(new SuspendComms());
                    var key = Manager.Redirect.CreateRedirectKey(session.GameAccount.Id, (session.Player.Guid as PlayerGuid).CreationBits);

                    await NetHandler.SendConnectTo(session, Manager.Redirect.Crypt, key, worldNode.IPAddress, worldNode.Port, 1);

                    // Enable key bindings, etc.
                    await session.Send(new AccountDataTimes { PlayerGuid = session.Player.Guid });

                    // Send known spells
                    await session.Send(new InitialKnownSpells
                    {
                        InitialLogin = character.FirstLogin == 1,
                        KnownSpells  = character.CharacterSpells
                    });

                    // Enter world.
                    Manager.Player.EnterWorld(session);
                }
            }
            else
                session.Dispose();
        }
    }
}

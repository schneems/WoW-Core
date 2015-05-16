// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Framework.Database.Character.Entities;
using Framework.Datastore;
using Framework.Objects;
using World.Shared.Game.Entities.Object;
using World.Shared.Game.Entities.Object.Descriptors;
using World.Shared.Game.Entities.Object.Guid;
using World.Shared.Game.Objects.Entities;

namespace World.Shared.Game.Entities
{
    public sealed class Player : WorldUnitBase, IWorldObject
    {
        public PlayerData PlayerData;

        Character data;

        public Player(Character player) : base(PlayerData.End)
        {
            data = player;

            Guid = new PlayerGuid
            {
                CreationBits = player.Guid,
                RealmId = (ushort)player.RealmId
            };

            PlayerData = new PlayerData();

            Position = new Vector3
            {
                X = player.X,
                Y = player.Y,
                Z = player.Z
            };

            Facing = player.O;
            Map = (short)player.Map;

            InitializeDescriptors();
        }

        public void InitializeDescriptors()
        {
            Set(ObjectData.Guid, Guid.Low);
            Set(ObjectData.Guid + 2, Guid.High);
            Set(ObjectData.Type, 0x19);
            Set(ObjectData.Scale, 1f);

            Set(UnitData.Health, 1337);
            Set(UnitData.MaxHealth, 1337);

            // Current experience level.
            Set(UnitData.Level, (uint)data.ExperienceLevel);

            // Current experience points & needed experience points for next level.
            Set(PlayerData.XP, data.Experience);
            Set(PlayerData.NextLevelXP, (int)ClientDB.GtOCTLevelExperience[0, data.ExperienceLevel]?.Data);

            var race = ClientDB.ChrRaces.Single(r => r.Id == data.Race);
            var chrClass = ClientDB.ChrClasses.Single(r => r.Id == data.Class);

            Set(UnitData.DisplayPower, chrClass.DisplayPower);
            Set(UnitData.FactionTemplate, race.FactionId);

            Set(UnitData.Sex, (byte)data.Race, 0);
            Set(UnitData.Sex, (byte)data.Class, 1);
            Set(UnitData.Sex, (byte)0, 2);
            Set(UnitData.Sex, data.Sex, 3);

            var displayId = data.Sex == 1 ? race.FemaleDisplayId : race.MaleDisplayId;

            Set(UnitData.DisplayID, displayId);
            Set(UnitData.NativeDisplayID, displayId);

            Set(UnitData.Flags, 0x8u);

            Set(UnitData.BoundingRadius, 0.389f);
            Set(UnitData.CombatReach, 1.5f);
            Set(UnitData.ModCastingSpeed, 1f);
            Set(UnitData.MaxHealthModifier, 1f);

            Set(PlayerData.HairColorID, data.Skin, 0);
            Set(PlayerData.HairColorID, data.Face, 1);
            Set(PlayerData.HairColorID, data.HairStyle, 2);
            Set(PlayerData.HairColorID, data.HairColor, 3);

            Set(PlayerData.RestState, data.FacialHairStyle, 0);
            Set(PlayerData.RestState, (byte)0, 1);
            Set(PlayerData.RestState, (byte)0, 2);
            Set(PlayerData.RestState, (byte)2, 3);

            Set(PlayerData.ArenaFaction, data.Sex, 0);
            Set(PlayerData.ArenaFaction, (byte)0, 1);
            Set(PlayerData.ArenaFaction, (byte)0, 2);
            Set(PlayerData.ArenaFaction, (byte)0, 3);

            Set(PlayerData.WatchedFactionIndex, -1);
            Set(PlayerData.VirtualPlayerRealm, data.RealmId);

            for (var i = 0; i < data.CharacterSkills.Count; i++)
                Set(PlayerData.Skill + i, data.CharacterSkills[i].SkillId);
        }

        public void InitializeDynamicDescriptors()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var guid = Guid as PlayerGuid;

            return $"Name: {data.Name}, Guid: {data.Guid}, RealmId: {data.RealmId}";
        }
    }
}

/*
 * Copyright (C) 2012-2015 Arctium Emulation <http://arctium.org>
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

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    class PlayerData : DescriptorBase
    {
        public PlayerData() : base(UnitData.End) { }

        public DescriptorField DuelArbiter                   => base[0x0, 0x4];
        public DescriptorField WowAccount                    => base[0x4, 0x4];
        public DescriptorField LootTargetGUID                => base[0x8, 0x4];
        public DescriptorField PlayerFlags                   => base[0xC, 0x1];
        public DescriptorField PlayerFlagsEx                 => base[0xD, 0x1];
        public DescriptorField GuildRankID                   => base[0xE, 0x1];
        public DescriptorField GuildDeleteDate               => base[0xF, 0x1];
        public DescriptorField GuildLevel                    => base[0x10, 0x1];
        public DescriptorField HairColorID                   => base[0x11, 0x1];
        public DescriptorField RestState                     => base[0x12, 0x1];
        public DescriptorField ArenaFaction                  => base[0x13, 0x1];
        public DescriptorField DuelTeam                      => base[0x14, 0x1];
        public DescriptorField GuildTimeStamp                => base[0x15, 0x1];
        public DescriptorField QuestLog                      => base[0x16, 0x2EE, MirrorFlags.Party];
        public DescriptorField VisibleItems                  => base[0x304, 0x39];
        public DescriptorField PlayerTitle                   => base[0x33D, 0x1];
        public DescriptorField FakeInebriation               => base[0x33E, 0x1];
        public DescriptorField VirtualPlayerRealm            => base[0x33F, 0x1];
        public DescriptorField CurrentSpecID                 => base[0x340, 0x1];
        public DescriptorField TaxiMountAnimKitID            => base[0x341, 0x1];
        public DescriptorField AvgItemLevelTotal             => base[0x342, 0x1];
        public DescriptorField AvgItemLevelEquipped          => base[0x343, 0x1];
        public DescriptorField CurrentBattlePetBreedQuality  => base[0x344, 0x1];
        public DescriptorField InvSlots                      => base[0x345, 0x2E0, MirrorFlags.Self];
        public DescriptorField FarsightObject                => base[0x625, 0x4, MirrorFlags.Self];
        public DescriptorField KnownTitles                   => base[0x629, 0xA, MirrorFlags.Self];
        public DescriptorField Coinage                       => base[0x633, 0x2, MirrorFlags.Self];
        public DescriptorField XP                            => base[0x635, 0x1, MirrorFlags.Self];
        public DescriptorField NextLevelXP                   => base[0x636, 0x1, MirrorFlags.Self];
        public DescriptorField Skill                         => base[0x637, 0x1C0, MirrorFlags.Self];
        public DescriptorField CharacterPoints               => base[0x7F7, 0x1, MirrorFlags.Self];
        public DescriptorField MaxTalentTiers                => base[0x7F8, 0x1, MirrorFlags.Self];
        public DescriptorField TrackCreatureMask             => base[0x7F9, 0x1, MirrorFlags.Self];
        public DescriptorField TrackResourceMask             => base[0x7FA, 0x1, MirrorFlags.Self];
        public DescriptorField MainhandExpertise             => base[0x7FB, 0x1, MirrorFlags.Self];
        public DescriptorField OffhandExpertise              => base[0x7FC, 0x1, MirrorFlags.Self];
        public DescriptorField RangedExpertise               => base[0x7FD, 0x1, MirrorFlags.Self];
        public DescriptorField CombatRatingExpertise         => base[0x7FE, 0x1, MirrorFlags.Self];
        public DescriptorField BlockPercentage               => base[0x7FF, 0x1, MirrorFlags.Self];
        public DescriptorField DodgePercentage               => base[0x800, 0x1, MirrorFlags.Self];
        public DescriptorField ParryPercentage               => base[0x801, 0x1, MirrorFlags.Self];
        public DescriptorField CritPercentage                => base[0x802, 0x1, MirrorFlags.Self];
        public DescriptorField RangedCritPercentage          => base[0x803, 0x1, MirrorFlags.Self];
        public DescriptorField OffhandCritPercentage         => base[0x804, 0x1, MirrorFlags.Self];
        public DescriptorField SpellCritPercentage           => base[0x805, 0x7, MirrorFlags.Self];
        public DescriptorField ShieldBlock                   => base[0x80C, 0x1, MirrorFlags.Self];
        public DescriptorField ShieldBlockCritPercentage     => base[0x80D, 0x1, MirrorFlags.Self];
        public DescriptorField Mastery                       => base[0x80E, 0x1, MirrorFlags.Self];
        public DescriptorField Amplify                       => base[0x80F, 0x1, MirrorFlags.Self];
        public DescriptorField Multistrike                   => base[0x810, 0x1, MirrorFlags.Self];
        public DescriptorField MultistrikeEffect             => base[0x811, 0x1, MirrorFlags.Self];
        public DescriptorField Readiness                     => base[0x812, 0x1, MirrorFlags.Self];
        public DescriptorField Speed                         => base[0x813, 0x1, MirrorFlags.Self];
        public DescriptorField Lifesteal                     => base[0x814, 0x1, MirrorFlags.Self];
        public DescriptorField Avoidance                     => base[0x815, 0x1, MirrorFlags.Self];
        public DescriptorField Sturdiness                    => base[0x816, 0x1, MirrorFlags.Self];
        public DescriptorField Cleave                        => base[0x817, 0x1, MirrorFlags.Self];
        public DescriptorField Versatility                   => base[0x818, 0x1, MirrorFlags.Self];
        public DescriptorField VersatilityBonus              => base[0x819, 0x1, MirrorFlags.Self];
        public DescriptorField PvpPowerDamage                => base[0x81A, 0x1, MirrorFlags.Self];
        public DescriptorField PvpPowerHealing               => base[0x81B, 0x1, MirrorFlags.Self];
        public DescriptorField ExploredZones                 => base[0x81C, 0xC8, MirrorFlags.Self];
        public DescriptorField RestStateBonusPool            => base[0x8E4, 0x1, MirrorFlags.Self];
        public DescriptorField ModDamageDonePos              => base[0x8E5, 0x7, MirrorFlags.Self];
        public DescriptorField ModDamageDoneNeg              => base[0x8EC, 0x7, MirrorFlags.Self];
        public DescriptorField ModDamageDonePercent          => base[0x8F3, 0x7, MirrorFlags.Self];
        public DescriptorField ModHealingDonePos             => base[0x8FA, 0x1, MirrorFlags.Self];
        public DescriptorField ModHealingPercent             => base[0x8FB, 0x1, MirrorFlags.Self];
        public DescriptorField ModHealingDonePercent         => base[0x8FC, 0x1, MirrorFlags.Self];
        public DescriptorField ModPeriodicHealingDonePercent => base[0x8FD, 0x1, MirrorFlags.Self];
        public DescriptorField WeaponDmgMultipliers          => base[0x8FE, 0x3, MirrorFlags.Self];
        public DescriptorField WeaponAtkSpeedMultipliers     => base[0x901, 0x3, MirrorFlags.Self];
        public DescriptorField ModSpellPowerPercent          => base[0x904, 0x1, MirrorFlags.Self];
        public DescriptorField ModResiliencePercent          => base[0x905, 0x1, MirrorFlags.Self];
        public DescriptorField OverrideSpellPowerByAPPercent => base[0x906, 0x1, MirrorFlags.Self];
        public DescriptorField OverrideAPBySpellPowerPercent => base[0x907, 0x1, MirrorFlags.Self];
        public DescriptorField ModTargetResistance           => base[0x908, 0x1, MirrorFlags.Self];
        public DescriptorField ModTargetPhysicalResistance   => base[0x909, 0x1, MirrorFlags.Self];
        public DescriptorField LocalFlags                    => base[0x90A, 0x1, MirrorFlags.Self];
        public DescriptorField LifetimeMaxRank               => base[0x90B, 0x1, MirrorFlags.Self];
        public DescriptorField SelfResSpell                  => base[0x90C, 0x1, MirrorFlags.Self];
        public DescriptorField PvpMedals                     => base[0x90D, 0x1, MirrorFlags.Self];
        public DescriptorField BuybackPrice                  => base[0x90E, 0xC, MirrorFlags.Self];
        public DescriptorField BuybackTimestamp              => base[0x91A, 0xC, MirrorFlags.Self];
        public DescriptorField YesterdayHonorableKills       => base[0x926, 0x1, MirrorFlags.Self];
        public DescriptorField LifetimeHonorableKills        => base[0x927, 0x1, MirrorFlags.Self];
        public DescriptorField WatchedFactionIndex           => base[0x928, 0x1, MirrorFlags.Self];
        public DescriptorField CombatRatings                 => base[0x929, 0x20, MirrorFlags.Self];
        public DescriptorField PvpInfo                       => base[0x949, 0x24, MirrorFlags.Self];
        public DescriptorField MaxLevel                      => base[0x96D, 0x1, MirrorFlags.Self];
        public DescriptorField RuneRegen                     => base[0x96E, 0x4, MirrorFlags.Self];
        public DescriptorField NoReagentCostMask             => base[0x972, 0x4, MirrorFlags.Self];
        public DescriptorField GlyphSlots                    => base[0x976, 0x6, MirrorFlags.Self];
        public DescriptorField Glyphs                        => base[0x97C, 0x6, MirrorFlags.Self];
        public DescriptorField GlyphSlotsEnabled             => base[0x982, 0x1, MirrorFlags.Self];
        public DescriptorField PetSpellPower                 => base[0x983, 0x1, MirrorFlags.Self];
        public DescriptorField Researching                   => base[0x984, 0xA, MirrorFlags.Self];
        public DescriptorField ProfessionSkillLine           => base[0x98E, 0x2, MirrorFlags.Self];
        public DescriptorField UiHitModifier                 => base[0x990, 0x1, MirrorFlags.Self];
        public DescriptorField UiSpellHitModifier            => base[0x991, 0x1, MirrorFlags.Self];
        public DescriptorField HomeRealmTimeOffset           => base[0x992, 0x1, MirrorFlags.Self];
        public DescriptorField ModPetHaste                   => base[0x993, 0x1, MirrorFlags.Self];
        public DescriptorField SummonedBattlePetGUID         => base[0x994, 0x4, MirrorFlags.Self];
        public DescriptorField OverrideSpellsID              => base[0x998, 0x1, MirrorFlags.UrgentSelfOnly | MirrorFlags.Self];
        public DescriptorField LfgBonusFactionID             => base[0x999, 0x1, MirrorFlags.Self];
        public DescriptorField LootSpecID                    => base[0x99A, 0x1, MirrorFlags.Self];
        public DescriptorField OverrideZonePVPType           => base[0x99B, 0x1, MirrorFlags.UrgentSelfOnly | MirrorFlags.Self];
        public DescriptorField ItemLevelDelta                => base[0x99C, 0x1, MirrorFlags.Self];
        public DescriptorField BagSlotFlags                  => base[0x99D, 0x4, MirrorFlags.Self];
        public DescriptorField BankBagSlotFlags              => base[0x9A1, 0x7, MirrorFlags.Self];
        public DescriptorField InsertItemsLeftToRight        => base[0x9A8, 0x1, MirrorFlags.Self];
        public DescriptorField QuestCompleted                => base[0x9A9, 0x271, MirrorFlags.Self];

        public static new int End => UnitData.End + 0xC1A;
    }
}

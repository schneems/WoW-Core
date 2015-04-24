// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    public class PlayerData : DescriptorBase
    {
        public PlayerData() : base(UnitData.End) { }

        public DescriptorField DuelArbiter                   => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField WowAccount                    => base[0x4, 0x4, MirrorFlags.All];
        public DescriptorField LootTargetGUID                => base[0x8, 0x4, MirrorFlags.All];
        public DescriptorField PlayerFlags                   => base[0xC, 0x1, MirrorFlags.All];
        public DescriptorField PlayerFlagsEx                 => base[0xD, 0x1, MirrorFlags.All];
        public DescriptorField GuildRankID                   => base[0xE, 0x1, MirrorFlags.All];
        public DescriptorField GuildDeleteDate               => base[0xF, 0x1, MirrorFlags.All];
        public DescriptorField GuildLevel                    => base[0x10, 0x1, MirrorFlags.All];
        public DescriptorField HairColorID                   => base[0x11, 0x1, MirrorFlags.All];
        public DescriptorField RestState                     => base[0x12, 0x1, MirrorFlags.All];
        public DescriptorField ArenaFaction                  => base[0x13, 0x1, MirrorFlags.All];
        public DescriptorField DuelTeam                      => base[0x14, 0x1, MirrorFlags.All];
        public DescriptorField GuildTimeStamp                => base[0x15, 0x1, MirrorFlags.All];
        public DescriptorField QuestLog                      => base[0x16, 0x2EE, MirrorFlags.Party];
        public DescriptorField VisibleItems                  => base[0x304, 0x26, MirrorFlags.All];
        public DescriptorField PlayerTitle                   => base[0x32A, 0x1, MirrorFlags.All];
        public DescriptorField FakeInebriation               => base[0x32B, 0x1, MirrorFlags.All];
        public DescriptorField VirtualPlayerRealm            => base[0x32C, 0x1, MirrorFlags.All];
        public DescriptorField CurrentSpecID                 => base[0x32D, 0x1, MirrorFlags.All];
        public DescriptorField TaxiMountAnimKitID            => base[0x32E, 0x1, MirrorFlags.All];
        public DescriptorField AvgItemLevelTotal             => base[0x32F, 0x1, MirrorFlags.All];
        public DescriptorField AvgItemLevelEquipped          => base[0x330, 0x1, MirrorFlags.All];
        public DescriptorField CurrentBattlePetBreedQuality  => base[0x331, 0x1, MirrorFlags.All];
        public DescriptorField InvSlots                      => base[0x332, 0x2E0, MirrorFlags.Self];
        public DescriptorField FarsightObject                => base[0x612, 0x4, MirrorFlags.Self];
        public DescriptorField KnownTitles                   => base[0x616, 0xA, MirrorFlags.Self];
        public DescriptorField Coinage                       => base[0x620, 0x2, MirrorFlags.Self];
        public DescriptorField XP                            => base[0x622, 0x1, MirrorFlags.Self];
        public DescriptorField NextLevelXP                   => base[0x623, 0x1, MirrorFlags.Self];
        public DescriptorField Skill                         => base[0x624, 0x1C0, MirrorFlags.Self];
        public DescriptorField CharacterPoints               => base[0x7E4, 0x1, MirrorFlags.Self];
        public DescriptorField MaxTalentTiers                => base[0x7E5, 0x1, MirrorFlags.Self];
        public DescriptorField TrackCreatureMask             => base[0x7E6, 0x1, MirrorFlags.Self];
        public DescriptorField TrackResourceMask             => base[0x7E7, 0x1, MirrorFlags.Self];
        public DescriptorField MainhandExpertise             => base[0x7E8, 0x1, MirrorFlags.Self];
        public DescriptorField OffhandExpertise              => base[0x7E9, 0x1, MirrorFlags.Self];
        public DescriptorField RangedExpertise               => base[0x7EA, 0x1, MirrorFlags.Self];
        public DescriptorField CombatRatingExpertise         => base[0x7EB, 0x1, MirrorFlags.Self];
        public DescriptorField BlockPercentage               => base[0x7EC, 0x1, MirrorFlags.Self];
        public DescriptorField DodgePercentage               => base[0x7ED, 0x1, MirrorFlags.Self];
        public DescriptorField ParryPercentage               => base[0x7EE, 0x1, MirrorFlags.Self];
        public DescriptorField CritPercentage                => base[0x7EF, 0x1, MirrorFlags.Self];
        public DescriptorField RangedCritPercentage          => base[0x7F0, 0x1, MirrorFlags.Self];
        public DescriptorField OffhandCritPercentage         => base[0x7F1, 0x1, MirrorFlags.Self];
        public DescriptorField SpellCritPercentage           => base[0x7F2, 0x7, MirrorFlags.Self];
        public DescriptorField ShieldBlock                   => base[0x7F9, 0x1, MirrorFlags.Self];
        public DescriptorField ShieldBlockCritPercentage     => base[0x7FA, 0x1, MirrorFlags.Self];
        public DescriptorField Mastery                       => base[0x7FB, 0x1, MirrorFlags.Self];
        public DescriptorField Amplify                       => base[0x7FC, 0x1, MirrorFlags.Self];
        public DescriptorField Multistrike                   => base[0x7FD, 0x1, MirrorFlags.Self];
        public DescriptorField MultistrikeEffect             => base[0x7FE, 0x1, MirrorFlags.Self];
        public DescriptorField Readiness                     => base[0x7FF, 0x1, MirrorFlags.Self];
        public DescriptorField Speed                         => base[0x800, 0x1, MirrorFlags.Self];
        public DescriptorField Lifesteal                     => base[0x801, 0x1, MirrorFlags.Self];
        public DescriptorField Avoidance                     => base[0x802, 0x1, MirrorFlags.Self];
        public DescriptorField Sturdiness                    => base[0x803, 0x1, MirrorFlags.Self];
        public DescriptorField Cleave                        => base[0x804, 0x1, MirrorFlags.Self];
        public DescriptorField Versatility                   => base[0x805, 0x1, MirrorFlags.Self];
        public DescriptorField VersatilityBonus              => base[0x806, 0x1, MirrorFlags.Self];
        public DescriptorField PvpPowerDamage                => base[0x807, 0x1, MirrorFlags.Self];
        public DescriptorField PvpPowerHealing               => base[0x808, 0x1, MirrorFlags.Self];
        public DescriptorField ExploredZones                 => base[0x809, 0xC8, MirrorFlags.Self];
        public DescriptorField RestStateBonusPool            => base[0x8D1, 0x1, MirrorFlags.Self];
        public DescriptorField ModDamageDonePos              => base[0x8D2, 0x7, MirrorFlags.Self];
        public DescriptorField ModDamageDoneNeg              => base[0x8D9, 0x7, MirrorFlags.Self];
        public DescriptorField ModDamageDonePercent          => base[0x8E0, 0x7, MirrorFlags.Self];
        public DescriptorField ModHealingDonePos             => base[0x8E7, 0x1, MirrorFlags.Self];
        public DescriptorField ModHealingPercent             => base[0x8E8, 0x1, MirrorFlags.Self];
        public DescriptorField ModHealingDonePercent         => base[0x8E9, 0x1, MirrorFlags.Self];
        public DescriptorField ModPeriodicHealingDonePercent => base[0x8EA, 0x1, MirrorFlags.Self];
        public DescriptorField WeaponDmgMultipliers          => base[0x8EB, 0x3, MirrorFlags.Self];
        public DescriptorField WeaponAtkSpeedMultipliers     => base[0x8EE, 0x3, MirrorFlags.Self];
        public DescriptorField ModSpellPowerPercent          => base[0x8F1, 0x1, MirrorFlags.Self];
        public DescriptorField ModResiliencePercent          => base[0x8F2, 0x1, MirrorFlags.Self];
        public DescriptorField OverrideSpellPowerByAPPercent => base[0x8F3, 0x1, MirrorFlags.Self];
        public DescriptorField OverrideAPBySpellPowerPercent => base[0x8F4, 0x1, MirrorFlags.Self];
        public DescriptorField ModTargetResistance           => base[0x8F5, 0x1, MirrorFlags.Self];
        public DescriptorField ModTargetPhysicalResistance   => base[0x8F6, 0x1, MirrorFlags.Self];
        public DescriptorField LocalFlags                    => base[0x8F7, 0x1, MirrorFlags.Self];
        public DescriptorField LifetimeMaxRank               => base[0x8F8, 0x1, MirrorFlags.Self];
        public DescriptorField SelfResSpell                  => base[0x8F9, 0x1, MirrorFlags.Self];
        public DescriptorField PvpMedals                     => base[0x8FA, 0x1, MirrorFlags.Self];
        public DescriptorField BuybackPrice                  => base[0x8FB, 0xC, MirrorFlags.Self];
        public DescriptorField BuybackTimestamp              => base[0x907, 0xC, MirrorFlags.Self];
        public DescriptorField YesterdayHonorableKills       => base[0x913, 0x1, MirrorFlags.Self];
        public DescriptorField LifetimeHonorableKills        => base[0x914, 0x1, MirrorFlags.Self];
        public DescriptorField WatchedFactionIndex           => base[0x915, 0x1, MirrorFlags.Self];
        public DescriptorField CombatRatings                 => base[0x916, 0x20, MirrorFlags.Self];
        public DescriptorField PvpInfo                       => base[0x936, 0x24, MirrorFlags.Self];
        public DescriptorField MaxLevel                      => base[0x95A, 0x1, MirrorFlags.Self];
        public DescriptorField RuneRegen                     => base[0x95B, 0x4, MirrorFlags.Self];
        public DescriptorField NoReagentCostMask             => base[0x95F, 0x4, MirrorFlags.Self];
        public DescriptorField GlyphSlots                    => base[0x963, 0x6, MirrorFlags.Self];
        public DescriptorField Glyphs                        => base[0x969, 0x6, MirrorFlags.Self];
        public DescriptorField GlyphSlotsEnabled             => base[0x96F, 0x1, MirrorFlags.Self];
        public DescriptorField PetSpellPower                 => base[0x970, 0x1, MirrorFlags.Self];
        public DescriptorField Researching                   => base[0x971, 0xA, MirrorFlags.Self];
        public DescriptorField ProfessionSkillLine           => base[0x97B, 0x2, MirrorFlags.Self];
        public DescriptorField UiHitModifier                 => base[0x97D, 0x1, MirrorFlags.Self];
        public DescriptorField UiSpellHitModifier            => base[0x97E, 0x1, MirrorFlags.Self];
        public DescriptorField HomeRealmTimeOffset           => base[0x97F, 0x1, MirrorFlags.Self];
        public DescriptorField ModPetHaste                   => base[0x980, 0x1, MirrorFlags.Self];
        public DescriptorField SummonedBattlePetGUID         => base[0x981, 0x4, MirrorFlags.Self];
        public DescriptorField OverrideSpellsID              => base[0x985, 0x1, MirrorFlags.Self | MirrorFlags.UrgentSelfOnly];
        public DescriptorField LfgBonusFactionID             => base[0x986, 0x1, MirrorFlags.Self];
        public DescriptorField LootSpecID                    => base[0x987, 0x1, MirrorFlags.Self];
        public DescriptorField OverrideZonePVPType           => base[0x988, 0x1, MirrorFlags.Self | MirrorFlags.UrgentSelfOnly];
        public DescriptorField ItemLevelDelta                => base[0x989, 0x1, MirrorFlags.Self];
        public DescriptorField BagSlotFlags                  => base[0x98A, 0x4, MirrorFlags.Self];
        public DescriptorField BankBagSlotFlags              => base[0x98E, 0x7, MirrorFlags.Self];
        public DescriptorField InsertItemsLeftToRight        => base[0x995, 0x1, MirrorFlags.Self];
        public DescriptorField QuestCompleted                => base[0x996, 0x271, MirrorFlags.Self];

        public static new int End => UnitData.End + 0xC07;
    }
}

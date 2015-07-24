// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using World.Shared.Constants.Objects;

namespace World.Shared.Game.Entities.Object.Descriptors
{
    public class UnitData : DescriptorBase
    {
        public UnitData() : base(ObjectData.End) { }

        public DescriptorField Charm                             => base[0x0, 0x4, MirrorFlags.All];
        public DescriptorField Summon                            => base[0x4, 0x4, MirrorFlags.All];
        public DescriptorField Critter                           => base[0x8, 0x4, MirrorFlags.Self];
        public DescriptorField CharmedBy                         => base[0xC, 0x4, MirrorFlags.All];
        public DescriptorField SummonedBy                        => base[0x10, 0x4, MirrorFlags.All];
        public DescriptorField CreatedBy                         => base[0x14, 0x4, MirrorFlags.All];
        public DescriptorField DemonCreator                      => base[0x18, 0x4, MirrorFlags.All];
        public DescriptorField Target                            => base[0x1C, 0x4, MirrorFlags.All];
        public DescriptorField BattlePetCompanionGUID            => base[0x20, 0x4, MirrorFlags.All];
        public DescriptorField BattlePetDBID                     => base[0x24, 0x2, MirrorFlags.All];
        public DescriptorField ChannelObject                     => base[0x26, 0x4, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField ChannelSpell                      => base[0x2A, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField ChannelSpellXSpellVisual          => base[0x2B, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField SummonedByHomeRealm               => base[0x2C, 0x1, MirrorFlags.All];
        public DescriptorField Sex                               => base[0x2D, 0x1, MirrorFlags.All];
        public DescriptorField DisplayPower                      => base[0x2E, 0x1, MirrorFlags.All];
        public DescriptorField OverrideDisplayPowerID            => base[0x2F, 0x1, MirrorFlags.All];
        public DescriptorField Health                            => base[0x30, 0x1, MirrorFlags.All];
        public DescriptorField Power                             => base[0x31, 0x6, MirrorFlags.All | MirrorFlags.UrgentSelfOnly];
        public DescriptorField MaxHealth                         => base[0x37, 0x1, MirrorFlags.All];
        public DescriptorField MaxPower                          => base[0x38, 0x6, MirrorFlags.All];
        public DescriptorField PowerRegenFlatModifier            => base[0x3E, 0x6, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.UnitAll];
        public DescriptorField PowerRegenInterruptedFlatModifier => base[0x44, 0x6, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.UnitAll];
        public DescriptorField Level                             => base[0x4A, 0x1, MirrorFlags.All];
        public DescriptorField EffectiveLevel                    => base[0x4B, 0x1, MirrorFlags.All];
        public DescriptorField FactionTemplate                   => base[0x4C, 0x1, MirrorFlags.All];
        public DescriptorField VirtualItems                      => base[0x4D, 0x6, MirrorFlags.All];
        public DescriptorField Flags                             => base[0x53, 0x1, MirrorFlags.All];
        public DescriptorField Flags2                            => base[0x54, 0x1, MirrorFlags.All];
        public DescriptorField Flags3                            => base[0x55, 0x1, MirrorFlags.All];
        public DescriptorField AuraState                         => base[0x56, 0x1, MirrorFlags.All];
        public DescriptorField AttackRoundBaseTime               => base[0x57, 0x2, MirrorFlags.All];
        public DescriptorField RangedAttackRoundBaseTime         => base[0x59, 0x1, MirrorFlags.Self];
        public DescriptorField BoundingRadius                    => base[0x5A, 0x1, MirrorFlags.All];
        public DescriptorField CombatReach                       => base[0x5B, 0x1, MirrorFlags.All];
        public DescriptorField DisplayID                         => base[0x5C, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField NativeDisplayID                   => base[0x5D, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField MountDisplayID                    => base[0x5E, 0x1, MirrorFlags.All | MirrorFlags.Urgent];
        public DescriptorField MinDamage                         => base[0x5F, 0x1, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.Empath];
        public DescriptorField MaxDamage                         => base[0x60, 0x1, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.Empath];
        public DescriptorField MinOffHandDamage                  => base[0x61, 0x1, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.Empath];
        public DescriptorField MaxOffHandDamage                  => base[0x62, 0x1, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.Empath];
        public DescriptorField AnimTier                          => base[0x63, 0x1, MirrorFlags.All];
        public DescriptorField PetNumber                         => base[0x64, 0x1, MirrorFlags.All];
        public DescriptorField PetNameTimestamp                  => base[0x65, 0x1, MirrorFlags.All];
        public DescriptorField PetExperience                     => base[0x66, 0x1, MirrorFlags.Owner];
        public DescriptorField PetNextLevelExperience            => base[0x67, 0x1, MirrorFlags.Owner];
        public DescriptorField ModCastingSpeed                   => base[0x68, 0x1, MirrorFlags.All];
        public DescriptorField ModSpellHaste                     => base[0x69, 0x1, MirrorFlags.All];
        public DescriptorField ModHaste                          => base[0x6A, 0x1, MirrorFlags.All];
        public DescriptorField ModRangedHaste                    => base[0x6B, 0x1, MirrorFlags.All];
        public DescriptorField ModHasteRegen                     => base[0x6C, 0x1, MirrorFlags.All];
        public DescriptorField CreatedBySpell                    => base[0x6D, 0x1, MirrorFlags.All];
        public DescriptorField NpcFlags                          => base[0x6E, 0x2, MirrorFlags.All | MirrorFlags.ViewerDependet];
        public DescriptorField EmoteState                        => base[0x70, 0x1, MirrorFlags.All];
        public DescriptorField Stats                             => base[0x71, 0x5, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField StatPosBuff                       => base[0x76, 0x5, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField StatNegBuff                       => base[0x7B, 0x5, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField Resistances                       => base[0x80, 0x7, MirrorFlags.Self | MirrorFlags.Owner | MirrorFlags.Empath];
        public DescriptorField ResistanceBuffModsPositive        => base[0x87, 0x7, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField ResistanceBuffModsNegative        => base[0x8E, 0x7, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField ModBonusArmor                     => base[0x95, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField BaseMana                          => base[0x96, 0x1, MirrorFlags.All];
        public DescriptorField BaseHealth                        => base[0x97, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField ShapeshiftForm                    => base[0x98, 0x1, MirrorFlags.All];
        public DescriptorField AttackPower                       => base[0x99, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField AttackPowerModPos                 => base[0x9A, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField AttackPowerModNeg                 => base[0x9B, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField AttackPowerMultiplier             => base[0x9C, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField RangedAttackPower                 => base[0x9D, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField RangedAttackPowerModPos           => base[0x9E, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField RangedAttackPowerModNeg           => base[0x9F, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField RangedAttackPowerMultiplier       => base[0xA0, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField MinRangedDamage                   => base[0xA1, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField MaxRangedDamage                   => base[0xA2, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField PowerCostModifier                 => base[0xA3, 0x7, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField PowerCostMultiplier               => base[0xAA, 0x7, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField MaxHealthModifier                 => base[0xB1, 0x1, MirrorFlags.Self | MirrorFlags.Owner];
        public DescriptorField HoverHeight                       => base[0xB2, 0x1, MirrorFlags.All];
        public DescriptorField MinItemLevelCutoff                => base[0xB3, 0x1, MirrorFlags.All];
        public DescriptorField MinItemLevel                      => base[0xB4, 0x1, MirrorFlags.All];
        public DescriptorField MaxItemLevel                      => base[0xB5, 0x1, MirrorFlags.All];
        public DescriptorField WildBattlePetLevel                => base[0xB6, 0x1, MirrorFlags.All];
        public DescriptorField BattlePetCompanionNameTimestamp   => base[0xB7, 0x1, MirrorFlags.All];
        public DescriptorField InteractSpellID                   => base[0xB8, 0x1, MirrorFlags.All];
        public DescriptorField StateSpellVisualID                => base[0xB9, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateAnimID                       => base[0xBA, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateAnimKitID                    => base[0xBB, 0x1, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField StateWorldEffectID                => base[0xBC, 0x4, MirrorFlags.ViewerDependet | MirrorFlags.Urgent];
        public DescriptorField ScaleDuration                     => base[0xC0, 0x1, MirrorFlags.All];
        public DescriptorField LooksLikeMountID                  => base[0xC1, 0x1, MirrorFlags.All];
        public DescriptorField LooksLikeCreatureID               => base[0xC2, 0x1, MirrorFlags.All];
        public DescriptorField LookAtControllerID                => base[0xC3, 0x1, MirrorFlags.All];
        public DescriptorField LookAtControllerTarget            => base[0xC4, 0x4, MirrorFlags.All];

        public static new int End => ObjectData.End + 0xC8;
    }
}

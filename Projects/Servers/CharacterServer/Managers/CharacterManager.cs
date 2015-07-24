// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Framework.Constants.Items;
using Framework.Database;
using Framework.Database.Character.Entities;
using Framework.Database.Data.Entities;
using Framework.Datastore;
using Framework.Misc;

namespace CharacterServer.Managers
{
    class CharacterManager : Singleton<CharacterManager>
    {
        CharacterManager()
        {

        }

        public void LearnStartAbilities(Character character)
        {
            var guid = DB.Character.Single<Character>(c => c.Name == character.Name).Guid;

            if (guid != 0)
            {
                var startAbilities = ClientDB.SkillLineAbilities.Where(a => a.AcquireMethod == 2 && a.SupercedesSpell == 0 &&
                                                                       a.CheckRaceClassConditions(character.RaceMask, character.ClassMask)).ToList();

                var spells = new ConcurrentDictionary<uint, CharacterSpell>();
                var skills = new ConcurrentDictionary<uint, CharacterSkill>();

                Parallel.ForEach(startAbilities, ability =>
                {
                    var spellLevels = ClientDB.SpellLevels[ability.Spell];

                    if (spellLevels.Count() == 0 || (spellLevels.Any(sl => sl.BaseLevel <= character.ExperienceLevel)))
                    {
                        SkillLine skillLine;

                        foreach (var srci in ClientDB.SkillRaceClassInfo.Where(srci => srci.SkillId == ability.SkillLine && srci.Availability == 1))
                        {
                            if (srci.CheckRaceClassConditions(character.RaceMask, character.ClassMask) && ClientDB.SkillLines.TryGetValue(ability.SkillLine, out skillLine))
                            {
                                var skillLevel = 1u;

                                // Set skill level based on category.
                                // Only languages handled for now.
                                // ToDo: Add an enum for that and handle the other categories.
                                switch (skillLine.CategoryID)
                                {
                                    case 10:
                                        skillLevel = 300;
                                        break;
                                    default:
                                        break;
                                }

                                spells.TryAdd(ability.Spell, new CharacterSpell
                                {
                                    CharacterGuid = guid,
                                    SpellId       = ability.Spell
                                });

                                skills.TryAdd(ability.SkillLine, new CharacterSkill
                                {
                                    CharacterGuid = guid,
                                    SkillId       = ability.SkillLine,
                                    SkillLevel    = skillLevel
                                });
                            }
                        }
                    }
                });

                DB.Character.Where<CharacterCreationSpell>(ccs => ccs.Race == character.Race && ccs.Class == character.Class).ForEach(ccs =>
                {
                    spells.TryAdd(ccs.SpellId, new CharacterSpell
                    {
                        CharacterGuid = guid,
                        SpellId       = ccs.SpellId
                    });
                });

                DB.Character.Where<CharacterCreationSkill>(ccs => ccs.Race == character.Race && ccs.Class == character.Class).ForEach(ccs =>
                {
                    skills.TryAdd(ccs.SkillId, new CharacterSkill
                    {
                        CharacterGuid = guid,
                        SkillId       = ccs.SkillId,
                        SkillLevel    = ccs.SkillLevel
                    });
                });

                DB.Character.Add(spells.Values.AsEnumerable());
                DB.Character.Add(skills.Values.AsEnumerable());
            }
        }

        public void AddStartItems(Character character)
        {
            var guid = DB.Character.Single<Character>(c => c.Name == character.Name).Guid;

            if (guid != 0)
            {
                var startItems = ClientDB.CharStartOutfits.SingleOrDefault(cso => cso.RaceId == character.Race && cso.ClassId == character.Class &&
                                                                           cso.SexId == character.Sex)?.ItemId;

                if (startItems != null)
                {
                    var items = new ConcurrentDictionary<uint, CharacterItem>();

                    for (var i = 0; i < startItems.Length; i++)
                    {
                        if (startItems[i] != 0)
                        {
                            var item = ClientDB.Items[startItems[i]];
                            var charItem = new CharacterItem
                            {
                                CharacterGuid = guid,
                                ItemId        = item.Id,
                                Bag           = 0,
                                Slot          = item.Slot,
                                Equipped      = true
                            };

                            if (item.InventoryType == (int)InventoryType.Usable)
                            {
                                // Default bag & bag slot
                                charItem.Bag      = 0xFF;
                                charItem.Slot     = (EquipmentSlot.Bag4 + 1);
                                charItem.Equipped = false;
                            }

                            items.TryAdd(item.Id, charItem);
                        }
                    }

                    DB.Character.Add(items.Values.AsEnumerable());
                }
            }
        }
    }
}

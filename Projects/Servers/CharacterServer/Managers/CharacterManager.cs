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
                                                                       a.CheckRaceClassConditions(character.RaceMask, character.ClassMask));

                var spells = new ConcurrentDictionary<uint, CharacterSpell>();
                var skills = new ConcurrentDictionary<uint, CharacterSkill>();

                Parallel.ForEach(startAbilities, ability =>
                {
                    SkillLine skillLine;

                    if (ClientDB.SkillLines.TryGetValue(ability.SkillLine, out skillLine))
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
                                charItem.Bag = 0xFF;
                                charItem.Slot = (EquipmentSlot.Bag4 + 1);
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

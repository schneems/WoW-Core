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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharacterServer.ObjectStores;
using Framework.Database;
using Framework.Database.Character.Entities;
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
                                                                       a.CheckRaceClassConditions(character.GetRaceMask(), character.GetClassMask()));

                var spellList = new List<CharacterSpell>();
                var skillList = new List<CharacterSkill>();

                Parallel.ForEach(startAbilities, ability =>
                {
                    var skillLine = ClientDB.SkillLines.SingleOrDefault(s => s.ID == ability.SkillLine);
                    var skillLevel = 1u;

                    if (skillLine != null)
                    {
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
                    }

                    spellList.Add(new CharacterSpell
                    {
                        CharacterGuid = guid,
                        SpellId       = ability.Spell
                    });

                    skillList.Add(new CharacterSkill
                    {
                        CharacterGuid = guid,
                        SkillId       = ability.SkillLine,
                        SkillLevel    = skillLevel
                    });
                });

                DB.Character.Add(spellList.AsEnumerable());
                DB.Character.Add(skillList.AsEnumerable());
            }
        }

        public void AddStartItems(Character character)
        {

        }
    }
}

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

namespace Framework.Constants.Spells
{
    [Flags]
    public enum SpellSchools : sbyte
    {
        Unknown      = 0,
        Physical     = 1 << SpellSchool.Physical,
        Holy         = 1 << SpellSchool.Holy,
        Fire         = 1 << SpellSchool.Fire,
        Nature       = 1 << SpellSchool.Nature,
        Frost        = 1 << SpellSchool.Frost,
        Shadow       = 1 << SpellSchool.Shadow,
        Arcane       = 1 << SpellSchool.Arcane,

        // Combined
        Holystrike   = Physical | Holy,
        Flamestrike  = Physical | Fire,
        Holyfire     = Holy     | Fire,
        Stormstrike  = Physical | Nature,
        Holystorm    = Holy     | Nature,
        Firestorm    = Fire     | Nature,
        Froststrike  = Physical | Frost,
        Holyfrost    = Holy     | Frost,
        Frostfire    = Fire     | Frost,
        Froststorm   = Nature   | Frost,
        Elemental    = Fire     | Nature | Frost,
        Shadowstrike = Physical | Shadow,
        Shadowlight  = Holy     | Shadow,
        Shadowflame  = Fire     | Shadow,
        Shadowstorm  = Nature   | Shadow,
        Shadowfrost  = Frost    | Shadow,
        Chromatic    = Holy     | Elemental | Shadow,
        Spellstrike  = Physical | Arcane,
        Divine       = Holy     | Arcane,
        Spellfire    = Fire     | Arcane,
        Spellstorm   = Nature   | Arcane,
        Spellfrost   = Frost    | Arcane,
        Spellshadow  = Shadow   | Arcane,
        Magic        = Chromatic| Arcane,
        Chaos        = Physical | Magic
    }
}

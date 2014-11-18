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
        Flamestrike  = Physical  | Fire,
        Froststrike  = Physical  | Frost,
        Spellstrike  = Physical  | Arcane,
        Shadowstrike = Physical  | Shadow,
        Stormstrike  = Physical  | Nature,
        Holystrike   = Physical  | Holy,
        Frostfire    = Fire      | Frost,
        Spellfire    = Fire      | Arcane,
        Firestorm    = Fire      | Nature,
        Shadowflame  = Fire      | Shadow,
        Holyfire     = Holy      | Fire,
        Spellfrost   = Frost     | Arcane,
        Froststorm   = Nature    | Frost,
        Shadowfrost  = Frost     | Shadow,
        Holyfrost    = Holy      | Frost,
        Spellstorm   = Nature    | Arcane,
        Spellshadow  = Shadow    | Arcane,
        Divine       = Holy      | Arcane,
        Shadowstorm  = Nature    | Shadow,
        Holystorm    = Holy      | Nature,
        Shadowlight  = Holy      | Shadow,
        Elemental    = Fire      | Nature    | Frost,
        Chromatic    = Holy      | Elemental | Shadow,
        Magic        = Chromatic | Arcane,
        Chaos        = Physical  | Magic
    }
}

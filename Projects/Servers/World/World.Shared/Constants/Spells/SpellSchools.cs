// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace World.Shared.Constants.Spells
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

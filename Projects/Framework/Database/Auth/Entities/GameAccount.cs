// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Constants.Account;
using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class GameAccount : Entity
    {
        [AutoIncrement]
        public uint Id                { get; set; }
        public uint AccountId         { get; set; }
        public string Game            { get; set; }
        public byte Index             { get; set; }
        public Region Region         { get; set; }
        public GameAccountFlags Flags { get; set; }
        public byte BoxLevel          { get; set; }
        public string OS              { get; set; }
        public string SessionKey      { get; set; }
        public bool IsOnline          { get; set; }

        public virtual IList<GameAccountRace> GameAccountRaces { get; set; }
        public virtual IList<GameAccountClass> GameAccountClasses { get; set; }
        public virtual IList<GameAccountCharacterTemplate> GameAccountCharacterTemplates { get; set; }
    }
}

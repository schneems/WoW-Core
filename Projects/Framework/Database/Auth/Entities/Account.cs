// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Constants.Account;
using Lappa_ORM;

namespace Framework.Database.Auth.Entities
{
    public class Account : Entity
    {
        [AutoIncrement]
        public uint Id                 { get; set; }
        public string GivenName        { get; set; }
        public string Surname          { get; set; }
        public string Email            { get; set; }
        public string Tag              { get; set; }
        public Region Region           { get; set; }
        public string Language         { get; set; }
        public AccountFlags Flags      { get; set; }
        public string PasswordVerifier { get; set; }
        public string Salt             { get; set; }
        public string IP               { get; set; }
        public byte LoginFailures      { get; set; }

        public virtual IList<GameAccount> GameAccounts { get; set; }
    }
}

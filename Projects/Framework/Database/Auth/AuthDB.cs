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

using System.Data;
using System.Data.Entity;
using Framework.Database.Auth.Entities;

namespace Framework.Database.Auth
{
    public class AuthDB : Database
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<GameAccount> GameAccounts { get; set; }
        public DbSet<AllowedClass> AllowedClasses { get; set; }
        public DbSet<AllowedRace> AllowedRaces { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Realm> Realms { get; set; }

        public AuthDB(IDbConnection conn) : base(conn) { }
    }
}

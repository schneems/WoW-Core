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

namespace Framework.Constants.Net
{
    // Value '0x2000' means not updated/implemented
    public enum GlobalServerMessage : ushort
    {
        AuthChallenge        = 0x0403,
        ConnectTo            = 0x0413,
        SuspendComms         = 0x068B,
        ResumeComms          = 0x068A,
        Pong                 = 0x2000,
        CharacterLoginFailed = 0x1722
    }
}

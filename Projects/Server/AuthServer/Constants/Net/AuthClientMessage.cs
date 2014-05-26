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

namespace AuthServer.Constants.Net
{
    public enum AuthClientMessage : ushort
    {
        #region None
        ProofResponse      = (0x02 + 0x3F) << AuthChannel.BattleNet,
        InformationRequest = (0x09 + 0x3F) << AuthChannel.BattleNet,
        #endregion
        #region Creep
        Ping               = (0x00 + 0x3F) << AuthChannel.Creep,
        Disconnect         = (0x06 + 0x3F) << AuthChannel.Creep,
        #endregion
        #region WoW
        RealmUpdate        = (0x00 + 0x3F) << AuthChannel.WoW,
        JoinRequest        = (0x08 + 0x3F) << AuthChannel.WoW,
        #endregion
    }
}

/*
 * Copyright (C) 2012-2014 Arctium <http://arctium.org>
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

namespace Framework.Constants.NetMessage
{
    public enum ServerMessage : ushort
    {
        #region Legacy
        ObjectUpdate                      = 0x1725,
        TutorialFlags                     = 0x10A7,
        StartCinematic                    = 0x04CC,
        #endregion

        #region JAMClientConnection
        AuthChallenge                     = 0x14B8,
        Pong                              = 0x15B1,
        #endregion

        #region JAMClientMove
        MoveUpdate                        = 0x1CB2,
        MoveSetCanFly                     = 0x01F4,
        MoveUnsetCanFly                   = 0x1D81,
        MoveSetWalkSpeed                  = 0x00A0,
        MoveSetRunSpeed                   = 0x1B9B,
        MoveSetSwimSpeed                  = 0x01D4,
        MoveSetFlightSpeed                = 0x02DC,
        MoveTeleport                      = 0x00D5,
        #endregion

        #region JAMClientGossip
        GossipMessage                     = 0x0E52,
        #endregion

        #region JAMClientSpell
        SendKnownSpells                   = 0x1B05,
        #endregion

        #region JAMClientDispatch
        QueryCreatureResponse             = 0x00E0,
        EnumCharactersResult              = 0x040A,
        LogoutComplete                    = 0x0429,
        TransferPending                   = 0x0440,
        NewWorld                          = 0x05AB,
        UnlearnedSpells                   = 0x05E3,
        QueryGameObjectResponse           = 0x066A,
        GenerateRandomCharacterNameResult = 0x074B,
        UITime                            = 0x0C22,
        UpdateTalentData                  = 0x0C68,
        LearnedSpells                     = 0x0C99,
        MOTD                              = 0x0E20,
        Chat                              = 0x0E60,
        AccountDataTimes                  = 0x0F40,
        LoginSetTimeSpeed                 = 0x0F4A,
        QueryNPCTextResponse              = 0x10E0,
        AddonInfo                         = 0x10E2,
        CreateChar                        = 0x1469,
        DeleteChar                        = 0x1529,
        AuthResponse                      = 0x15A0,
        RealmQueryResponse                = 0x1652,
        UpdateActionButtons               = 0x1768,
        DestroyObject                     = 0x1D69,
        CacheVersion                      = 0x1E41,
        QueryPlayerNameResponse           = 0x1E5B,
        DBReply                           = 0x1F01,
        #endregion

        TransferInitiate                  = 0x4F57,
    }
}

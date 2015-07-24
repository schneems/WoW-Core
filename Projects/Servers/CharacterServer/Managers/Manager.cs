// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CharacterServer.Managers
{
    class Manager
    {
        public static CharacterManager Character;
        public static ClientDBManager ClientDB;
        public static GameAccountManager GameAccount;
        public static RedirectManager Redirect;

        public static void Initialize()
        {
            Character   = CharacterManager.GetInstance();
            ClientDB    = ClientDBManager.GetInstance();
            GameAccount = GameAccountManager.GetInstance();
            Redirect    = RedirectManager.GetInstance();
        }
    }
}

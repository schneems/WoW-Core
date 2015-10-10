// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldServer.Managers
{
    class Manager
    {
        public static CharacterManager Character;
        public static ClientDBManager ClientDB;
        public static GameAccountManager GameAccount;
        public static RedirectManager Redirect;
        public static SessionManager Session;
        public static PlayerManager Player;
        public static SpawnManager Spawns;

        public static void Initialize()
        {
            Character   = CharacterManager.GetInstance();
            ClientDB    = ClientDBManager.GetInstance();
            GameAccount = GameAccountManager.GetInstance();
            Redirect    = RedirectManager.GetInstance();
            Session     = SessionManager.GetInstance();
            Player      = PlayerManager.GetInstance();
            Spawns      = SpawnManager.GetInstance();
        }
    }
}

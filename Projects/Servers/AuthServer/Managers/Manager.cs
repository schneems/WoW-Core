// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AuthServer.Managers
{
    class Manager
    {
        public static ModuleManager ModuleMgr;
        public static PatchManager PatchMgr;
        public static RealmManager RealmMgr;
        public static SessionManager SessionMgr;

        public static void Initialize()
        {
            ModuleMgr  = ModuleManager.GetInstance();
            PatchMgr   = PatchManager.GetInstance();
            RealmMgr   = RealmManager.GetInstance();
            SessionMgr = SessionManager.GetInstance();
        }

        public static bool GetState()
        {
            var state = false;
            var nullState = ModuleMgr != null && RealmMgr != null && SessionMgr != null;

            if (nullState)
                state = nullState && (ModuleMgr.IsInitialized && RealmMgr.IsInitialized && SessionMgr.IsInitialized);

            return state;
        }
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WorldNode.Managers
{
    class Manager
    {
        public static RedirectManager Redirect;
        public static SessionManager Session;

        public static void Initialize()
        {
            Redirect = RedirectManager.GetInstance();
            Session  = SessionManager.GetInstance();
        }
    }
}

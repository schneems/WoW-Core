// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BnetServer.Constants.Service
{
    // TODO: Implement connection state handling in ServiceSession.
    public enum BnetServiceConnectionState
    {
        None = 0,
        Connect = 1,
        Logon = 2,
        GameAccountSelection = 3,
        RealmSelection = 4,
        Ready = 5,
        Error = 6
    }
}

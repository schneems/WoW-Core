// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BnetServer.Constants.Bnet
{
    // TODO: Implement connection state handling in BnetSession.
    public enum ConnectionState
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

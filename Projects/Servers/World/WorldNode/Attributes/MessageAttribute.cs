// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Framework.Constants.Account;
using WorldNode.Constants.Net;

namespace WorldNode.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MessageAttribute : Attribute
    {
        public ClientMessage Message { get; }
        public SessionState State { get; }

        public MessageAttribute(ClientMessage message, SessionState state)
        {
            Message = message;
            State = state;
        }
    }
}

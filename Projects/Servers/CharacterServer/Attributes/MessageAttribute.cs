// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using CharacterServer.Constants.Net;
using Framework.Constants.Account;

namespace CharacterServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MessageAttribute : Attribute
    {
        public ClientMessage Message { get; }
        public SessionState State    { get; }

        public MessageAttribute(ClientMessage message, SessionState state)
        {
            Message = message;
            State = state;
        }
    }
}

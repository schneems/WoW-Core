// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Framework.Constants.Account;
using Framework.Constants.Net;

namespace Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class GlobalMessageAttribute : Attribute
    {
        public GlobalClientMessage Message { get; }
        public SessionState State          { get; }

        public GlobalMessageAttribute(GlobalClientMessage message, SessionState state)
        {
            Message = message;
            State = state;
        }
    }
}

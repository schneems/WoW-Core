// Copyright (c) Multi-Emu.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using AuthServer.Constants.Net;

namespace AuthServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AuthMessageAttribute : Attribute
    {
        public AuthClientMessage Message { get; }
        public AuthChannel Channel { get; }

        public AuthMessageAttribute(AuthClientMessage message, AuthChannel channel)
        {
            Message = message;
            Channel = channel;
        }
    }
}

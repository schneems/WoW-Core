// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WorldNode.Constants.Net;

namespace WorldNode.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MessageAttribute : Attribute
    {
        public ClientMessage Message { get; }

        public MessageAttribute(ClientMessage message)
        {
            Message = message;
        }
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Framework.Constants.IPC;

namespace Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class IPCMessageAttribute : Attribute
    {
        public IPCMessage Message { get; set; }

        public IPCMessageAttribute(IPCMessage message)
        {
            Message = message;
        }
    }
}

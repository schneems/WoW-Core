// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BnetServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BnetServiceMethodAttribute : Attribute
    {
        public uint MethodId { get; set; }
    }
}

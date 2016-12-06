// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BnetServer.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RestServiceAttribute : Attribute
    {
        public string Host { get; set; }
    }
}

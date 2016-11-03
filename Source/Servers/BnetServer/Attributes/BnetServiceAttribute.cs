// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BnetServer.Constants.Bnet;

namespace BnetServer.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BnetServiceAttribute : Attribute
    {
        public BnetServiceHash Hash { get; set; }
    }
}

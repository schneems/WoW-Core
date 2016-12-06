// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BnetServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RestRouteAttribute : Attribute
    {
        // GET or POST
        public string Method { get; set; }
        public string Path { get; set; }
    }
}

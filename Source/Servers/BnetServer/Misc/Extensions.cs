// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Bgs.Protocol;
using Bgs.Protocol.GameUtilities.V1;
using System.Linq;

namespace BnetServer.Misc
{
    static class Extensions
    {
        public static Variant GetVariant(this ClientRequest clientRequest, string attributeName)
        {
            return clientRequest.Attribute.SingleOrDefault(a => a.Name == attributeName)?.Value;
        }
    }
}

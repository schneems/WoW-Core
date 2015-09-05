// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Framework.Constants.Object;

namespace Framework.Objects
{
    public class SmartGuid
    {
        public ulong Low  { get; set; }
        public ulong High { get; set; }

        public virtual GuidType Type
        {
            get { return (GuidType)(High >> 58); }
            set { High |= (ulong)value << 58; }
        }
    }
}

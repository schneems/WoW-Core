// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Packets.Json.Forms
{
    [DataContract]
    public class FormInputs
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "prompt")]
        public string Prompt { get; set; }

        [DataMember(Name = "inputs")]
        public List<FormInput> Inputs { get; set; }
    }
}

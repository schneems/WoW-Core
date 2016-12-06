// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;
using Framework.Packets.Json.Forms;

namespace Framework.Packets.Json.Authentication
{
    [DataContract]
    public class LogonResult
    {
        [DataMember(Name = "authentication_state")]
        public string AuthenticationState { get; set; }

        [DataMember(Name = "login_ticket")]
        public string LoginTicket { get; set; }

        [DataMember(Name = "error_code")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "error_message")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "support_error_code")]
        public string SupportErrorCode { get; set; }

        [DataMember(Name = "authenticator_form")]
        public FormInputs AuthenticatorForm { get; set; }
    }
}

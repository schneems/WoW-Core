// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.Constants.Authentication;
using Framework.Constants.Web;
using Framework.Packets.Json.Authentication;
using Framework.Packets.Json.Forms;
using Framework.Serialization;
using Framework.Web;

namespace BnetServer.Packets.Rest
{
    public static class RestResponse
    {
        public static byte[] LoginForm { get; }
        public static byte[] AuthenticatorForm { get; }
        public static byte[] InvalidAccountOrCredentials { get; }

        static RestResponse()
        {
            // Create the login form.
            var loginForm = new FormInputs
            {
                Type = "LOGIN_FORM",
                Inputs = new List<FormInput>
                {
                    // AccountForm
                    new FormInput
                    {
                        Id = "account_name",
                        Type = "text",
                        Label = "Battle.net email",
                        MaxLength = 320
                    },
                    // PasswordForm
                    new FormInput
                    {
                        Id = "password",
                        Type = "password",
                        Label = "Password",
                        MaxLength = 100
                    },
                    // SubmitForm
                    new FormInput
                    {
                        Id = "log_in_submit",
                        Type = "submit",
                        Label = "Log in to Battle.net"
                    }
                }
            };

            LoginForm = HttpResponse.Create(HttpCode.Ok, Json.CreateString(loginForm));

            // Create the authenticator logon result.
            var authenticatorForm = new LogonResult
            {
                AuthenticationState = AuthenticationState.Authenticator,
                AuthenticatorForm = new FormInputs
                {
                    Type = "AUTHENTICATOR_FORM",
                    Prompt = "Enter the security code from your authenticator.",
                    Inputs = new List<FormInput>
                    {
                        // SecurityCodeForm
                        new FormInput
                        {
                            Id = "authenticator_input",
                            Type = "text",
                            Label = "Security Code",
                            MaxLength = 20
                        },
                        // RememberForm
                        new FormInput
                        {
                            Id = "remember_authenticator",
                            Type = "checkbox",
                            Label = "Don't ask me again"
                        },
                        // SubmitForm
                        new FormInput
                        {
                            Id = "authenticator_submit",
                            Type = "submit",
                            Label = "Submit"
                        }
                    }
                }
            };

            AuthenticatorForm = HttpResponse.Create(HttpCode.Ok, Json.CreateString(authenticatorForm));

            var logonResult = new LogonResult
            {
                AuthenticationState = AuthenticationState.Login,
                ErrorCode = "INVALID_ACCOUNT_OR_CREDENTIALS",
                ErrorMessage = "We couldn't log you in with what you just entered. Please try again.",
            };

            InvalidAccountOrCredentials = HttpResponse.Create(HttpCode.BadRequest, Json.CreateString(logonResult), true);
        }
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Web
{
    public class HttpHeader
    {
        public string Method         { get; set; }
        public string Path           { get; set; }
        public string Type           { get; set; }
        public string Host           { get; set; }
        public string DeviceId       { get; set; }
        public string ContentType    { get; set; }
        public int ContentLength     { get; set; }
        public string AcceptLanguage { get; set; }
        public string Accept         { get; set; }
        public string UserAgent      { get; set; }
        public string Content        { get; set; }

        // Custom header var. This is a random generated session value.
        public Guid PathValue { get; set; }
    }
}

// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Text;
using Framework.Constants.Web;

namespace Framework.Web
{
    public class HttpResponse
    {
        public static byte[] Create(HttpCode httpCode, string content, bool closeConnection = false)
        {
            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                sw.WriteLine($"HTTP/1.1 {(int)httpCode} {httpCode}");

                sw.WriteLine($"Date: {DateTime.Now.ToUniversalTime():r}");
                sw.WriteLine("Server: Arctium-Emulation");
                sw.WriteLine("Retry-After: 600");
                sw.WriteLine($"Content-Length: {content.Length}");
                sw.WriteLine("Vary: Accept-Encoding");

                if (closeConnection)
                    sw.WriteLine("Connection: close");

                sw.WriteLine("Content-Type: application/json;charset=UTF-8");
                sw.WriteLine();

                sw.WriteLine(content);
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}

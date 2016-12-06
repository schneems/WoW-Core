// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Framework.Web
{
    public class HttpRequest
    {
        public static HttpHeader Parse(byte[] data, int length)
        {
            var headerValues = new Dictionary<string, object>();
            var header = new HttpHeader();

            using (var sr = new StreamReader(new MemoryStream(data, 0, length)))
            {
                var info = sr.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (info.Length != 3)
                    return null;

                headerValues.Add("method", info[0]);
                headerValues.Add("path", info[1]);
                headerValues.Add("type", info[2]);

                while (!sr.EndOfStream)
                {
                    info = sr.ReadLine().Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

                    if (info.Length == 2)
                        headerValues.Add(info[0].Replace("-", "").ToLower(), info[1]);
                    else if (info.Length > 2)
                    {
                        var val = "";

                        info.Skip(1);

                        headerValues.Add(info[0].Replace("-", "").ToLower(), val);
                    }
                    else
                    {
                        // We are at content here.
                        var content = sr.ReadLine();

                        headerValues.Add("content", content);

                        // There shouldn't be anything after the content!
                        break;
                    }
                }
            }

            var httpFields = typeof(HttpHeader).GetTypeInfo().GetProperties();

            foreach (var f in httpFields)
            {
                object val;

                if (headerValues.TryGetValue(f.Name.ToLower(), out val))
                {
                    if (f.PropertyType == typeof(int))
                        f.SetValue(header, Convert.ChangeType(Convert.ToInt32(val), f.PropertyType));
                    else
                        f.SetValue(header, Convert.ChangeType(val, f.PropertyType));
                }
            }

            return header;
        }
    }
}

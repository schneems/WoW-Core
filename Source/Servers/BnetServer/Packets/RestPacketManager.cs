// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BnetServer.Attributes;
using BnetServer.Network;
using Framework.Logging;
using Framework.Misc;
using Framework.Web;

namespace BnetServer.Packets
{
    public class RestPacketManager : Singleton<RestPacketManager>
    {
        readonly ConcurrentDictionary<string, Dictionary<Tuple<string, string>, MethodInfo>> restServiceHandlers;

        RestPacketManager()
        {
            restServiceHandlers = new ConcurrentDictionary<string, Dictionary<Tuple<string, string>, MethodInfo>>();

            var assembly = Assembly.GetEntryAssembly();

            foreach (var type in assembly.GetTypes().Where(t => t.GetTypeInfo().GetCustomAttribute<RestServiceAttribute>() != null))
            {
                var restMethods = new Dictionary<Tuple<string, string>, MethodInfo>();

                foreach (var method in type.GetMethods().Where(m => m.GetCustomAttribute<RestRouteAttribute>() != null))
                {
                    var methodAttributeInfo = method.GetCustomAttribute<RestRouteAttribute>();

                    restMethods.Add(Tuple.Create(methodAttributeInfo.Method, methodAttributeInfo.Path), method);
                }

                var serviceAttributeInfo = type.GetTypeInfo().GetCustomAttribute<RestServiceAttribute>();

                if (restServiceHandlers.TryAdd(serviceAttributeInfo.Host, restMethods))
                {
                    Log.Message(LogTypes.Info, $"Registered REST handlers for {serviceAttributeInfo.Host}:");

                    foreach (var m in restMethods)
                        Log.Message(LogTypes.Info, $"- Method: {m.Key.Item1}, Path: {m.Key.Item2}");
                }
            }

            Log.NewLine();
        }

        public async void CallService(HttpHeader request, BnetChallengeSession session)
        {
            var pathInfo = request.Path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var pathValueMatch = Regex.Match(pathInfo[1], "^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$");

            if (pathInfo.Length != 2)
            {
                Log.Message(LogTypes.Error, $"Got invalid path '{request.Path}'.");

                session.Dispose();
                return;
            }

            Guid sessionGuid;

            if (!pathValueMatch.Success || !Guid.TryParse(pathValueMatch.Captures[0].Value, out sessionGuid))
            {
                Log.Message(LogTypes.Error, $"Wrong path value for '{request.Host}{request.Path}'.");

                session.Dispose();
                return;
            }

            if (!Manager.Session.Exists(sessionGuid))
            {
                Log.Message(LogTypes.Error, $"Session with guid '{pathValueMatch.Captures[0].Value}' doesn't exists..");

                session.Dispose();
                return;
            }

            request.PathValue = sessionGuid;

            Dictionary<Tuple<string, string>, MethodInfo> restMethods;

            // TODO: Fix host configuration in RestServiceAttribute.
            //if (restServiceHandlers.TryGetValue(request.Host, out restMethods))
            if(restServiceHandlers.TryGetValue("*", out restMethods))
            {
                var methodInfo = Tuple.Create(request.Method, pathInfo[0]);

                MethodInfo method;

                if (restMethods.TryGetValue(methodInfo, out method))
                {
                    // TODO: Test for performance issues and problems with async/await.
                    await Task.Run(() => method.Invoke(null, new object[] { request, session }));
                }
                else
                    Log.Message(LogTypes.Error, $"Got unhandled REST method/path '{methodInfo.Item1}/{methodInfo.Item2}' for '{request.Host}'.");
            }
            else
                Log.Message(LogTypes.Error, $"Got unhandled REST service for host '{request.Host}'.");
        }
    }
}

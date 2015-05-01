// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AuthServer.Network.Sessions;
using Framework.Constants.Account;
using Framework.Database;
using Framework.Database.Auth.Entities;
using Framework.Logging;
using Framework.Misc;
using Framework.Network.Packets;

namespace AuthServer.Managers
{
    class ModuleManager : Singleton<ModuleManager>
    {
        public readonly List<Module> Modules;

        ModuleManager()
        {
            Modules = new List<Module>();

            UpdateModules();
        }

        public void UpdateModules()
        {
            Log.Normal("Loading auth modules...");

            var modules = DB.Auth.Select<Module>();

            modules.ForEach(m =>
            {
                if (AddModule(m, Modules))
                    Log.Debug("New auth module '{0}, {1}' loaded.", m.Name, m.System);
            });

            Log.Normal($"Successfully loaded {Modules.Count} auth modules.");
            Log.Message();

            IsInitialized = true;
        }

        bool AddModule(Module module, IList list)
        {
            if (!list.Contains(module))
                return list.Add(module) != -1;

            return false;
        }

        public void WriteModuleHeader(Client client, AuthPacket packet, Module module, int size = 0)
        {
            packet.WriteFourCC(module.Type);
            packet.WriteFourCC("\0\0" + Enum.GetName(typeof(Region), client.Session.Account.Region));
            packet.Write(module.Hash.ToByteArray());
            packet.Write(size == 0 ? module.Size : (uint)size, 10);
        }

        public void WriteRiskFingerprint(Client client, AuthPacket packet)
        {
            var riskFingerprintModule = client.Modules.SingleOrDefault(m => m.Name == "RiskFingerprint");

            WriteModuleHeader(client, packet, riskFingerprintModule);
        }
    }
}

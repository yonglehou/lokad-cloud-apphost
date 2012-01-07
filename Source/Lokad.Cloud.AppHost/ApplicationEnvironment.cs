﻿#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Framework.Commands;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost
{
    /// <remarks>This class need to be able to cross AppDomains by reference, so all method arguments need to be serializable!</remarks>
    internal class ApplicationEnvironment : MarshalByRefObject, IApplicationEnvironment
    {
        private readonly IHostContext _hostContext;
        private readonly CellHandle _cellHandle;
        private readonly Action<IHostCommand> _sendCommand;

        internal ApplicationEnvironment(IHostContext hostContext, CellHandle cellHandle, Action<IHostCommand> sendCommand)
        {
            _hostContext = hostContext;
            _cellHandle = cellHandle;
            _sendCommand = sendCommand;
        }

        public string WorkerName
        {
            get { return _hostContext.WorkerName; }
        }

        public string UniqueWorkerInstanceName
        {
            get { return _hostContext.UniqueWorkerInstanceName; }
        }

        public string CellName
        {
            get { return _cellHandle.CellName; }
        }

        public string UniqueCellInstanceName
        {
            get { return _cellHandle.CurrentUniqueCellInstanceName; }
        }

        public string SolutionName
        {
            get { return _cellHandle.SolutionName; }
        }

        public SolutionHead CurrentDeployment
        {
            get { return _cellHandle.CurrentDeployment; }
        }

        public AssembliesHead CurrentAssemblies
        {
            get { return _cellHandle.CurretAssemblies; }
        }

        public void LoadDeployment(SolutionHead deployment)
        {
            _sendCommand(new LoadDeploymentCommand(deployment));
        }

        public void LoadCurrentHeadDeployment()
        {
            _sendCommand(new LoadCurrentHeadDeploymentCommand());
        }

        public int CurrentWorkerInstanceCount
        {
            get { return _hostContext.CurrentWorkerInstanceCount; }
        }

        public void ProvisionWorkerInstances(int numberOfInstances)
        {
            _hostContext.ProvisionWorkerInstances(numberOfInstances);
        }

        public void ProvisionWorkerInstancesAtLeast(int minNumberOfInstances)
        {
            _hostContext.ProvisionWorkerInstancesAtLeast(minNumberOfInstances);
        }

        public string GetSettingValue(string settingName)
        {
            return _hostContext.GetSettingValue(settingName);
        }

        public X509Certificate2 GetCertificate(string thumbprint)
        {
            return _hostContext.GetCertificate(thumbprint);
        }

        public string GetLocalResourcePath(string resourceName)
        {
            return _hostContext.GetLocalResourcePath(resourceName);
        }

        public IPEndPoint GetEndpoint(string endpointName)
        {
            return _hostContext.GetEndpoint(endpointName);
        }

        public void SendCommand(IHostCommand command)
        {
            _sendCommand(command);
        }
    }
}

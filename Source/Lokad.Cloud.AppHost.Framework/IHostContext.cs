﻿#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.Net;
using System.Security.Cryptography.X509Certificates;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework
{
    /// <summary>
    /// Provides the host with access to its environment (i.e. the RoleEnvironment in Windows Azure),
    /// and most importantly with a reader to poll and read deployments from some arbitrary storage provider.
    /// </summary>
    /// <remarks>Implementation does not need to be serializable (other than the deployment reader).</remarks>
    public interface IHostContext
    {
        HostInstanceIdentity Identity { get; }
        CellInstanceIdentity GetNewUniqueCellIdentity(string solutionName, string cellName, SolutionHead deployment);

        string GetSettingValue(string settingName);
        X509Certificate2 GetCertificate(string thumbprint);
        string GetLocalResourcePath(string resourceName);

        IPEndPoint GetEndpoint(string endpointName);

        int CurrentWorkerInstanceCount { get; }
        void ProvisionWorkerInstances(int numberOfInstances);
        void ProvisionWorkerInstancesAtLeast(int minNumberOfInstances);

        IDeploymentReader DeploymentReader { get; }

        /// <remarks>Can be <c>null</c>.</remarks>
        IHostObserver Observer { get; }
    }
}

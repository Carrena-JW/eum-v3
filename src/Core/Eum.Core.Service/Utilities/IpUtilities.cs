using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Utilities
{
    public static class IpUtilities
    {
        private const ushort MIN_PORT = 1;
        private const ushort MAX_PORT = UInt16.MaxValue;
        public static int? GetAvailablePort(ushort lowerPort = MIN_PORT, ushort upperPort = MAX_PORT)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var usedPorts = Enumerable.Empty<int>()
                .Concat(ipProperties.GetActiveTcpConnections().Select(c => c.LocalEndPoint.Port))
                .Concat(ipProperties.GetActiveTcpListeners().Select(l => l.Port))
                .Concat(ipProperties.GetActiveUdpListeners().Select(l => l.Port))
                .ToHashSet();
            for (int port = lowerPort; port <= upperPort; port++)
            {
                if (!usedPorts.Contains(port)) return port;
            }
            return null;
        }
    }
}

using Microsoft.Extensions.Hosting;

namespace Eum.Core
{
    public static class CoreExtensions
    {
        public static IHost ConfigureEumCore(this IHost host)
        {
            Static.ServiceProvider = host.Services;
            return host;
        }
    }
}
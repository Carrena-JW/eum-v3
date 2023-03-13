using Eum.Core.Service.Contracts.Auth;
using Eum.Core.Service.Contracts.ServiceDesk;
using Microsoft.Extensions.DependencyInjection;

namespace Eum.Core.Service
{
    public static class GrpcClientExtensions
    {
        public static void AddAuthClient(this IServiceCollection services)
        {
            services.AddTransient<IAccountClient, AccountClient>();
        }
        public static void AddServiceDeskClient(this IServiceCollection services)
        {
            services.AddTransient<IServiceDeskClient, ServiceDeskClient>();
        }
    }
}

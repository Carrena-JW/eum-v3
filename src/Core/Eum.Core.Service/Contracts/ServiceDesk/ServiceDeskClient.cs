using Eum.Core.Service.Contracts.ServiceDesk.Endpoints;
using Microsoft.AspNetCore.Http;

namespace Eum.Core.Service.Contracts.ServiceDesk
{
    public interface IServiceDeskClient
    {
        IStepEndpoint Step { get; }
        IProductEndpoint Product { get; }
    }

    public class ServiceDeskClient : GrpcClientBase, IServiceDeskClient
    {
        public IStepEndpoint Step { get; }
        public IProductEndpoint Product { get; }

        public ServiceDeskClient(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            Step = CreateClient<IStepEndpoint>(token);
            Product = CreateClient<IProductEndpoint>(token);
        }

        protected override string GetAddress()
        {
            return "https://localhost:7074";
        }
    }
}

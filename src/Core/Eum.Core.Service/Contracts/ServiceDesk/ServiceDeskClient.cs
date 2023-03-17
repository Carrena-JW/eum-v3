using Eum.Core.Service.Contracts.ServiceDesk.ClientModule;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule;
using Eum.Core.Service.Contracts.ServiceDesk.ProductModule;
using Eum.Core.Service.Contracts.ServiceDesk.StepModule;
using Microsoft.AspNetCore.Http;

namespace Eum.Core.Service.Contracts.ServiceDesk
{
    public interface IServiceDeskClient
    {
        IStepEndpoint Step { get; }
        IProductEndpoint Product { get; }
        ICompanyEndpoint Company { get; }
        IClientEndpoint Client { get; }
    }

    public class ServiceDeskClient : GrpcClientBase, IServiceDeskClient
    {
        public IStepEndpoint Step { get; }
        public IProductEndpoint Product { get; }
        public ICompanyEndpoint Company { get; }
        public IClientEndpoint Client { get; }

        public ServiceDeskClient(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            Step = CreateClient<IStepEndpoint>(token);
            Product = CreateClient<IProductEndpoint>(token);
            Company = CreateClient<ICompanyEndpoint>(token);
            Client = CreateClient<IClientEndpoint>(token);
        }

        protected override string GetAddress()
        {
            return "https://localhost:7074";
        }
    }
}

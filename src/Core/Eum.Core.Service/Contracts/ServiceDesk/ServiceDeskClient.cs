using Eum.Core.Service.Contracts.ServiceDesk.ClientModule;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule;
using Eum.Core.Service.Contracts.ServiceDesk.ContractModule;
using Eum.Core.Service.Contracts.ServiceDesk.ProductModule;
using Microsoft.AspNetCore.Http;

namespace Eum.Core.Service.Contracts.ServiceDesk
{
    /// <summary>
    /// 서비스데스크 클라이언트 인터페이스
    /// </summary>
    public interface IServiceDeskClient
    {
        /// <summary>
        /// 제품 관리
        /// </summary>
        IProductEndpoint Product { get; }
        /// <summary>
        /// 회사 관리
        /// </summary>
        ICompanyEndpoint Company { get; }
        /// <summary>
        /// 고객 관리
        /// </summary>
        IClientEndpoint Client { get; }
        /// <summary>
        /// 계약 관리
        /// </summary>
        IContractEndpoint Contract { get; }
    }

    /// <summary>
    /// 서비스데스크 클라이언트
    /// </summary>
    public class ServiceDeskClient : GrpcClientBase, IServiceDeskClient
    {
        /// <summary>
        /// 제품 관리
        /// </summary>
        public IProductEndpoint Product { get; }
        /// <summary>
        /// 회사 관리
        /// </summary>
        public ICompanyEndpoint Company { get; }
        /// <summary>
        /// 고객 관리
        /// </summary>
        public IClientEndpoint Client { get; }
        /// <summary>
        /// 계약 관리
        /// </summary>
        public IContractEndpoint Contract { get; }

        public ServiceDeskClient(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            Product = CreateClient<IProductEndpoint>(token);
            Company = CreateClient<ICompanyEndpoint>(token);
            Client = CreateClient<IClientEndpoint>(token);
            Contract = CreateClient<IContractEndpoint>(token);
        }

        /// <summary>
        /// 서버 연결 정보를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        protected override string GetAddress()
        {
            return "https://localhost:7074";
        }
    }
}

using Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.CompanyModule
{
    [ServiceContract]
    public interface ICompanyEndpoint : IEndpoint
    {
        [OperationContract]
        Task<GetCompanyListReply> GetCompanyListAsync(GetCompanyListRequest request, CallContext context = default);


        [OperationContract]
        Task<SetCompanyReply> SetCompanyAsync(SetCompanyRequest request, CallContext context = default);


        [OperationContract]
        Task<DelCompanyReply> DelCompanyAsync(DelCompanyRequest request, CallContext context = default);
    }

    /// <summary>
    /// 클래스 주석
    /// </summary>
    [Area("ServiceDesk")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class CompanyController : Controller
    {
        private IServiceDeskClient _serviceDeskClient;

        /// <summary>
        /// 생성자 주석
        /// </summary>
        /// <param name="serviceDeskClient"></param>
        public CompanyController(IServiceDeskClient serviceDeskClient)
        {
            _serviceDeskClient = serviceDeskClient;
        }

        /// <summary>
        /// GET 주석
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetCompanyListReply> GetCompanyList()
        {
            var results = await _serviceDeskClient.Company.GetCompanyListAsync(new GetCompanyListRequest());
            return results;
        }

        /// <summary>
        /// POST 주석
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SetCompanyReply> SetCompanyList(SetCompanyRequest request)
        {
            var results = await _serviceDeskClient.Company.SetCompanyAsync(request);
            return results;
        }

        /// <summary>
        /// DEL 주석
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<DelCompanyReply> DelCompanyList(Guid id)
        {
            var results = await _serviceDeskClient.Company.DelCompanyAsync(new DelCompanyRequest { Id = id });
            return results;
        }
    }
}
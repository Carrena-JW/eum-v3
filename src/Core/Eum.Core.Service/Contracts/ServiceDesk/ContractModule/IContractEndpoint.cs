using Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ContractModule
{
    [ServiceContract]
    public interface IContractEndpoint : IEndpoint
    {
        [OperationContract]
        Task<GetContractListReply> GetContractListAsync(GetContractListRequest request, CallContext context = default);


        [OperationContract]
        Task<SetContractReply> SetContractAsync(SetContractRequest request, CallContext context = default);


        [OperationContract]
        Task<DelContractReply> DelContractAsync(DelContractRequest request, CallContext context = default);
    }

    /// <summary>
    /// 클래스 주석
    /// </summary>
    [Area("ServiceDesk")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class ContractController : Controller
    {
        private IServiceDeskClient _serviceDeskClient;

        /// <summary>
        /// 생성자 주석
        /// </summary>
        /// <param name="serviceDeskClient"></param>
        public ContractController(IServiceDeskClient serviceDeskClient)
        {
            _serviceDeskClient = serviceDeskClient;
        }

        /// <summary>
        /// GET 주석
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetContractListReply> GetContractList()
        {
            var results = await _serviceDeskClient.Contract.GetContractListAsync(new GetContractListRequest());
            return results;
        }

        /// <summary>
        /// POST 주석
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SetContractReply> SetContractList(SetContractRequest request)
        {
            var results = await _serviceDeskClient.Contract.SetContractAsync(request);
            return results;
        }

        /// <summary>
        /// DEL 주석
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<DelContractReply> DelContractList(Guid id)
        {
            var results = await _serviceDeskClient.Contract.DelContractAsync(new DelContractRequest { Id = id });
            return results;
        }
    }
}

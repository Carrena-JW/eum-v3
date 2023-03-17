using Eum.Core.Service.Contracts.ServiceDesk.StepModule.Data;
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

namespace Eum.Core.Service.Contracts.ServiceDesk.StepModule
{
    [ServiceContract]
    public interface IStepEndpoint : IEndpoint
    {
        [OperationContract]
        Task<GetStepListReply> GetStepListAsync(GetStepListRequest request, CallContext context = default);


        [OperationContract]
        Task<SetStepReply> SetStepAsync(SetStepRequest request, CallContext context = default);


        [OperationContract]
        Task<DelStepReply> DelStepAsync(DelStepRequest request, CallContext context = default);
    }

    /// <summary>
    /// 클래스 주석
    /// </summary>
    [Area("ServiceDesk")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class StepController : Controller
    {
        private IServiceDeskClient _serviceDeskClient;

        /// <summary>
        /// 생성자 주석
        /// </summary>
        /// <param name="serviceDeskClient"></param>
        public StepController(IServiceDeskClient serviceDeskClient)
        {
            _serviceDeskClient = serviceDeskClient;
        }

        /// <summary>
        /// GET 주석
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetStepListReply> GetStepList()
        {
            var results = await _serviceDeskClient.Step.GetStepListAsync(new GetStepListRequest());
            return results;
        }

        /// <summary>
        /// POST 주석
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SetStepReply> SetStepList(SetStepRequest request)
        {
            var results = await _serviceDeskClient.Step.SetStepAsync(request);
            return results;
        }

        /// <summary>
        /// DEL 주석
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<DelStepReply> DelStepList(Guid id)
        {
            var results = await _serviceDeskClient.Step.DelStepAsync(new DelStepRequest { Id = id });
            return results;
        }
    }
}

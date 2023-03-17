using Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ClientModule
{
    [ServiceContract]
    public interface IClientEndpoint : IEndpoint
    {
        [OperationContract]
        Task<GetClientListReply> GetClientListAsync(GetClientListRequest request, CallContext context = default);


        [OperationContract]
        Task<SetClientReply> SetClientAsync(SetClientRequest request, CallContext context = default);


        [OperationContract]
        Task<DelClientReply> DelClientAsync(DelClientRequest request, CallContext context = default);
    }

    /// <summary>
    /// 클래스 주석
    /// </summary>
    [Area("ServiceDesk")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class ClientController : Controller
    {
        private IServiceDeskClient _serviceDeskClient;

        /// <summary>
        /// 생성자 주석
        /// </summary>
        /// <param name="serviceDeskClient"></param>
        public ClientController(IServiceDeskClient serviceDeskClient)
        {
            _serviceDeskClient = serviceDeskClient;
        }

        /// <summary>
        /// GET 주석
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetClientListReply> GetClientList()
        {
            var results = await _serviceDeskClient.Client.GetClientListAsync(new GetClientListRequest());
            return results;
        }

        /// <summary>
        /// POST 주석
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SetClientReply> SetClientList(SetClientRequest request)
        {
            var results = await _serviceDeskClient.Client.SetClientAsync(request);
            return results;
        }

        /// <summary>
        /// DEL 주석
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<DelClientReply> DelClientList(Guid id)
        {
            var results = await _serviceDeskClient.Client.DelClientAsync(new DelClientRequest { Id = id });
            return results;
        }
    }
}
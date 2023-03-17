using Eum.Core.Service.Contracts.ServiceDesk.ProductModule.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ProductModule
{
    [ServiceContract]
    public interface IProductEndpoint : IEndpoint
    {
        [OperationContract]
        Task<GetProductListReply> GetProductListAsync(GetProductListRequest request, CallContext context = default);


        [OperationContract]
        Task<SetProductReply> SetProductAsync(SetProductRequest request, CallContext context = default);


        [OperationContract]
        Task<DelProductReply> DelProductAsync(DelProductRequest request, CallContext context = default);
    }

    /// <summary>
    /// 클래스 주석
    /// </summary>
    [Area("ServiceDesk")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : Controller
    {
        private IServiceDeskClient _serviceDeskClient;

        /// <summary>
        /// 생성자 주석
        /// </summary>
        /// <param name="serviceDeskClient"></param>
        public ProductController(IServiceDeskClient serviceDeskClient)
        {
            _serviceDeskClient = serviceDeskClient;
        }

        /// <summary>
        /// GET 주석
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetProductListReply> GetProductList()
        {
            var results = await _serviceDeskClient.Product.GetProductListAsync(new GetProductListRequest());
            return results;
        }

        /// <summary>
        /// POST 주석
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SetProductReply> SetProductList(SetProductRequest request)
        {
            var results = await _serviceDeskClient.Product.SetProductAsync(request);
            return results;
        }

        /// <summary>
        /// DEL 주석
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<DelProductReply> DelProductList(Guid id)
        {
            var results = await _serviceDeskClient.Product.DelProductAsync(new DelProductRequest { Id = id });
            return results;
        }
    }
}
using AutoMapper;
using Eum.Core.Service.Contracts.ServiceDesk.Endpoints;
using Eum.Core.Service.Contracts.ServiceDesk.Product;
using Eum.Core.Service.Contracts.ServiceDesk.Product.Params;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Repositories;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Domains;

namespace Eum.gRPC.Server.ServiceDesk.Endpoints.ProductModule
{
    [Authorize]
    public class ProductEndpoint : IProductEndpoint
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductEndpoint(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<DelProductReply> DelProductAsync(DelProductRequest request, CallContext context = default)
        {
            var result = _productRepository.Del(request.Id);
            return Task.FromResult(new DelProductReply { Succeed = Convert.ToBoolean(result) });
        }

        public Task<GetProductListReply> GetProductListAsync(GetProductListRequest request, CallContext context = default)
        {
            var items = _productRepository.Get();
            return Task.FromResult(new GetProductListReply
            {
                Items = items.Select(x => _mapper.Map<ProductDTO>(x)).ToArray()
            });
        }

        public Task<SetProductReply> SetProductAsync(SetProductRequest request, CallContext context = default)
        {
            var item = _productRepository.Set(_mapper.Map<Product>(request.Item));
            return Task.FromResult(new SetProductReply
            {
                Item = item is null ? null : _mapper.Map<ProductDTO>(item)
            });
        }
    }
}

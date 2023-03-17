using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Grpc.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Repositories;
using Eum.Core.Service.Contracts.ServiceDesk.ProductModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.ProductModule;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ProductModule
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

        public static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<Product, SetProductRequest>().ReverseMap();
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
            var data = _mapper.Map<Product>(request);
            data.CreatedId = context.ServerCallContext.GetHttpContext().User.GetPersonCode();
            var item = _productRepository.Set(data);
            return Task.FromResult(new SetProductReply
            {
                Item = item is null ? null : _mapper.Map<ProductDTO>(item)
            });
        }
    }
}

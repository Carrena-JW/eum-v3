using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Grpc.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Repositories;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Repositories;
using Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.ClientModule;

namespace Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule
{
    [Authorize]
    public class CompanyEndpoint : ICompanyEndpoint
    {
        private ICompanyRepository _CompanyRepository;
        private IMapper _mapper;

        public CompanyEndpoint(ICompanyRepository CompanyRepository, IMapper mapper)
        {
            _CompanyRepository = CompanyRepository;
            _mapper = mapper;
        }

        public static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Company, CompanyDTO>().ReverseMap();
            cfg.CreateMap<Company, SetCompanyRequest>().ReverseMap();
        }

        public Task<DelCompanyReply> DelCompanyAsync(DelCompanyRequest request, CallContext context = default)
        {
            var result = _CompanyRepository.Del(request.Id);
            return Task.FromResult(new DelCompanyReply { Succeed = Convert.ToBoolean(result) });
        }

        public Task<GetCompanyListReply> GetCompanyListAsync(GetCompanyListRequest request, CallContext context = default)
        {
            var items = _CompanyRepository.Get();
            return Task.FromResult(new GetCompanyListReply
            {
                Items = items.Select(x => _mapper.Map<CompanyDTO>(x)).ToArray()
            });
        }

        public Task<SetCompanyReply> SetCompanyAsync(SetCompanyRequest request, CallContext context = default)
        {
            var data = _mapper.Map<Company>(request);
            data.CreatedId = context.ServerCallContext.GetHttpContext().User.GetPersonCode();
            var item = _CompanyRepository.Set(data);
            return Task.FromResult(new SetCompanyReply
            {
                Item = item is null ? null : _mapper.Map<CompanyDTO>(item)
            });
        }
    }
}

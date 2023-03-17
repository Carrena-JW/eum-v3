using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Grpc.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Repositories;
using Eum.Core.Service.Contracts.ServiceDesk.ClientModule;
using Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Domains;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ClientModule
{
    [Authorize]
    public class ClientEndpoint : IClientEndpoint
    {
        private IClientRepository _ClientRepository;
        private IMapper _mapper;

        public ClientEndpoint(IClientRepository ClientRepository, IMapper mapper)
        {
            _ClientRepository = ClientRepository;
            _mapper = mapper;
        }

        public static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Client, ClientDTO>().ReverseMap();
            cfg.CreateMap<Client, SetClientRequest>().ReverseMap();
        }

        public Task<DelClientReply> DelClientAsync(DelClientRequest request, CallContext context = default)
        {
            var result = _ClientRepository.Del(request.Id);
            return Task.FromResult(new DelClientReply { Succeed = Convert.ToBoolean(result) });
        }

        public Task<GetClientListReply> GetClientListAsync(GetClientListRequest request, CallContext context = default)
        {
            var items = _ClientRepository.Get();
            return Task.FromResult(new GetClientListReply
            {
                Items = items.Select(x => _mapper.Map<ClientDTO>(x)).ToArray()
            });
        }

        public Task<SetClientReply> SetClientAsync(SetClientRequest request, CallContext context = default)
        {
            var data = _mapper.Map<Client>(request);
            data.CreatedId = context.ServerCallContext.GetHttpContext().User.GetPersonCode();
            var item = _ClientRepository.Set(data);
            return Task.FromResult(new SetClientReply
            {
                Item = item is null ? null : _mapper.Map<ClientDTO>(item)
            });
        }
    }
}

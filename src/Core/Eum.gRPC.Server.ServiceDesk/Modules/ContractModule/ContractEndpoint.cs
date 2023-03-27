using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using AutoMapper;
using Eum.Core.Service.Contracts.ServiceDesk.ContractModule;
using Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.ContractModule.Repositories;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ContractModule
{
    [Authorize]
    public class ContractEndpoint : IContractEndpoint
    {
        private IContractRepository _stepRepository;

        public ContractEndpoint(IContractRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        public static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {

        }

        public Task<GetContractListReply> GetContractListAsync(GetContractListRequest request, CallContext context = default)
        {
            return Task.FromResult(new GetContractListReply
            {
                Items = _stepRepository.Get()
            });
        }

        public Task<SetContractReply> SetContractAsync(SetContractRequest request, CallContext context = default)
        {
            var result = _stepRepository.Set(request.Item);
            return Task.FromResult(new SetContractReply
            {
                Item = result
            });
        }

        public Task<DelContractReply> DelContractAsync(DelContractRequest request, CallContext context = default)
        {
            var result = _stepRepository.Del(request.Id);
            return Task.FromResult(new DelContractReply { Succeed = result });
        }
    }
}

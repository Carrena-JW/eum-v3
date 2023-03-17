﻿using Eum.Core.Service.Contracts.ServiceDesk.Data.Step.Params;
using Eum.Core.Service.Contracts.ServiceDesk.Endpoints;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.gRPC.Server.ServiceDesk.Modules.StepModule.Repositories;

namespace Eum.gRPC.Server.ServiceDesk.Endpoints.Step
{
    [Authorize]
    public class StepEndpoint : IStepEndpoint
    {
        private IStepRepository _stepRepository;

        public StepEndpoint(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository;
        }

        public Task<GetStepListReply> GetStepListAsync(GetStepListRequest request, CallContext context = default)
        {
            return Task.FromResult(new GetStepListReply
            {
                Items = _stepRepository.Get()
            });
        }

        public Task<SetStepReply> SetStepAsync(SetStepRequest request, CallContext context = default)
        {
            var result = _stepRepository.Set(request.Item);
            return Task.FromResult(new SetStepReply
            {
                Item = result
            });
        }

        public Task<DelStepReply> DelStepAsync(DelStepRequest request, CallContext context = default)
        {
            var result = _stepRepository.Del(request.Id);
            return Task.FromResult(new DelStepReply { Succeed = result });
        }
    }
}

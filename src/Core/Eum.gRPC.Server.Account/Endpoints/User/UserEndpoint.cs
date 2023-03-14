using Eum.Core.Shared.Services;
using Eum.Core.Service.Contracts.Account.Data.User;
using Eum.Core.Service.Contracts.Account.Endpoints;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Grpc.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;

namespace Eum.gRPC.Server.Account.Endpoints.User
{
    [Authorize]
    public class UserEndpoint : IUserEndpoint
    {
        private IPersonService _personService;

        public UserEndpoint(IPersonService personService)
        {
            _personService = personService;
        }

        public Task<GetUserContextReply> GetUserContextAsync(GetUserContextRequest request, CallContext context = default)
        {
            var user = context.ServerCallContext.GetHttpContext().User;
            var personCode = user.GetPersonCode();
            var userData = _personService.GetPerson(personCode);
            return Task.FromResult(new GetUserContextReply
            {
                User = userData
            });
        }
    }
}

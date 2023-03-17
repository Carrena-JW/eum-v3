using Eum.Core.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using Grpc.Core;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.Core.Service.Contracts.Account.UserModule.Data;
using Eum.Core.Service.Contracts.Account.UserModule;

namespace Eum.gRPC.Server.Account.Modules.UserModule
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

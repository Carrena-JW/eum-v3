using Eum.Core.Service.Contracts.Account.Endpoints;
using Microsoft.AspNetCore.Http;

namespace Eum.Core.Service.Contracts.Auth
{
    public interface IAccountClient
    {
        IAuthEndpoint Auth { get; }
        ITokenEndpoint Token { get; }
    }

    public class AccountClient : GrpcClientBase, IAccountClient
    {
        public IAuthEndpoint Auth { get; }
        public ITokenEndpoint Token { get; }

        public AccountClient(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            Auth = CreateClient<IAuthEndpoint>(token);
            Token = CreateClient<ITokenEndpoint>(token);
        }

        protected override string GetAddress()
        {
            return "https://localhost:7073";
        }
    }
}

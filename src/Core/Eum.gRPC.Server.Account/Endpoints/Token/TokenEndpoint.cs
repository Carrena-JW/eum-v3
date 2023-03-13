using Eum.Core.Shared.Services;
using Eum.Core.Service.Contracts.Auth.Data.Token;
using ProtoBuf.Grpc;
using Microsoft.AspNetCore.Authorization;
using Eum.Core.Service.Contracts.Account.Endpoints;

namespace Eum.gRPC.Server.Account.Endpoints.Token
{
    [Authorize]
    public class TokenEndpoint : ITokenEndpoint
    {
        private ITokenService _tokenService;

        public TokenEndpoint(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenReply> CreateAsync(TokenRequest request, CallContext context = default)
        {
            var token = await _tokenService.CreateAsync(request.UserName);
            return new TokenReply
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            };
        }
    }
}

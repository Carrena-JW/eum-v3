using Eum.Core.Shared.Services;
using ProtoBuf.Grpc;
using Microsoft.AspNetCore.Authorization;
using Eum.Core.Service.Contracts.Account.TokenModule.Data;
using Eum.Core.Service.Contracts.Account.TokenModule;

namespace Eum.gRPC.Server.Account.Modules.TokenModule
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

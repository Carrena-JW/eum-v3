using Eum.gRPC.Server.Auth.Modules.Token.Services;
using Eum.ServiceClient.Contracts.Auth.Data.Token;
using Eum.ServiceClient.Contracts.Auth.Endpoints;
using ProtoBuf.Grpc;

namespace Eum.gRPC.Server.Auth.Endpoints.Token
{
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

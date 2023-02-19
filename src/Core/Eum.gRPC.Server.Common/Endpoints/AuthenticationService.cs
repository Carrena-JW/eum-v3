using Grpc.Core;
using static Eum.gRPC.Server.Common.Authentication;

namespace Eum.gRPC.Server.Common.Endpoints
{
    public class AuthenticationEndpoint : AuthenticationBase
    {
        private readonly ILogger<AuthenticationEndpoint> _logger;
        public AuthenticationEndpoint(ILogger<AuthenticationEndpoint> logger)
        {
            _logger = logger;
        }

        public override Task<AuthenticationReplay> Login(AuthenticationRequest request, ServerCallContext context)
        {

            _logger.LogInformation("[Login] recived");
            return Task.FromResult(new AuthenticationReplay
            {
                Result = true
            }) ;
        }
    }
}
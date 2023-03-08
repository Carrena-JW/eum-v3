using Eum.gRPC.Server.ServiceDesk.Modules.Case.Services;
using Grpc.Core;

namespace Eum.gRPC.Server.ServiceDesk.Endpoints
{
    public class GreeterEndpoint : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterEndpoint> _logger;
        private readonly ITestService _testService;

        public GreeterEndpoint(ILogger<GreeterEndpoint> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + ", " + _testService.GetTestData()
            });
        }
    }
}
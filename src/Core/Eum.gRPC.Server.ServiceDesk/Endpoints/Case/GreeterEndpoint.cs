using Eum.gRPC.Server.ServiceDesk.Modules.Case.Services;
using Eum.ServiceClient.Contracts.ServiceDesk.Endpoints;
using ProtoBuf.Grpc;

namespace Eum.gRPC.Server.ServiceDesk.Endpoints.Case
{
    public class GreeterEndpoint : IGreeterEndpoint
    {
        private readonly ILogger<GreeterEndpoint> _logger;
        private readonly ITestService _testService;

        public GreeterEndpoint(ILogger<GreeterEndpoint> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }
        public Task<HelloReply> SayHello2Async(HelloRequest request, CallContext context)
        {
            var items = _testService.GetTestData();
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + ", Count: " + items.Count(),
                Items = items.ToArray()
            });
        }
    }
}
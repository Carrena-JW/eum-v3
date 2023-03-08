using Eum.Core.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.Case.Repositories;

namespace Eum.gRPC.Server.ServiceDesk.Modules.Case.Services
{
    public interface ITestService : IService 
    {
        string GetTestData();
    }
    public class TestService : ITestService
    {
        public TestService(ITestRepository testRepository) 
        {
        }

        public string GetTestData()
        {
            return "I'm a test service.";
        }
    }
}

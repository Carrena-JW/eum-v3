using Eum.Core.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.Case.Repositories;
using Eum.ServiceClient.Contracts.ServiceDesk.Data.Case;

namespace Eum.gRPC.Server.ServiceDesk.Modules.Case.Services
{
    public interface ITestService : IService 
    {
        IEnumerable<CaseInfo> GetTestData();
    }
    public class TestService : ITestService
    {
        private ITestRepository _testRepository;

        public TestService(ITestRepository testRepository) 
        {
            _testRepository = testRepository;
        }

        public IEnumerable<CaseInfo> GetTestData()
        {
            return _testRepository.GetCases();
        }
    }
}

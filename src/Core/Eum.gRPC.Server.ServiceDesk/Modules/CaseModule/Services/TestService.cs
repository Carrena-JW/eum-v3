using Eum.Core.Data;
using Eum.Core.Service.Contracts.ServiceDesk.Data.Case;
using Eum.gRPC.Server.ServiceDesk.Modules.CaseModule.Repositories;

namespace Eum.gRPC.Server.ServiceDesk.Modules.CaseModule.Services
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

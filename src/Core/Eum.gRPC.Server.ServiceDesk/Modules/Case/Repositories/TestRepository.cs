using Eum.Core.Data;
using Eum.Core.Service.Contracts.ServiceDesk.Data.Case;

namespace Eum.gRPC.Server.ServiceDesk.Modules.Case.Repositories
{
    public interface ITestRepository : IRepository
    {
        IEnumerable<CaseInfo> GetCases();
    }

    public class TestRepository : DatabaseRepositoryBase, ITestRepository
    {
        public TestRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public IEnumerable<CaseInfo> GetCases()
        {
            return base.ExecuteQuery<CaseInfo>(
                "SELECT * FROM [T_CASE]");
        }
    }
}

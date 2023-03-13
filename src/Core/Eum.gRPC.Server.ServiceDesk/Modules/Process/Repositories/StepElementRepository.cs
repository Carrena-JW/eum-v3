using Eum.Core.Data;
using Eum.Core.Service.Contracts.ServiceDesk.Data.Case;

namespace Eum.gRPC.Server.ServiceDesk.Modules.Process.Repositories
{
    public interface IStepElementRepository : IRepository
    {
    }

    public class StepElementRepository : DatabaseRepositoryBase, IStepElementRepository
    {
        public StepElementRepository(IConfiguration configuration) : base("EumServiceDesk", configuration)
        {
        }

        public void Set()
        {

        }

        public void Get()
        {

        }

        public void Del()
        {

        }
    }
}

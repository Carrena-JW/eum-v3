using Eum.Core.Data;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ProcessModule.Repositories
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

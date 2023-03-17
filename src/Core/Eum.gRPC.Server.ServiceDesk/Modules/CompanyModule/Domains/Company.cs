using System.Runtime.Serialization;

namespace Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Domains
{
    public class Company
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedId { get; set; }
    }
}

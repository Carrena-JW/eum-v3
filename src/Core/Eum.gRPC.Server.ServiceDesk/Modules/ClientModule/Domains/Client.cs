using System.Runtime.Serialization;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains
{
    public class Client
    {
        public Guid? Id { get; set; }

        public string Email { get; set; }

        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public bool CanLogin { get; set; } = false;

        public string Contact { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedId { get; set; }
    }
}

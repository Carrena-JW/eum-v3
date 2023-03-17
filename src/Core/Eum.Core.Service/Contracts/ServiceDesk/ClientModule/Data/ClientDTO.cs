using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data
{
    [DataContract]
    public class ClientDTO
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }

        [DataMember(Order = 2)]
        public string Email { get; set; }

        [DataMember(Order = 3)]
        public Guid CompanyId { get; set; }

        [DataMember(Order = 4)]
        public string Name { get; set; }

        [DataMember(Order = 5)]
        public bool CanLogin { get; set; } = false;

        [DataMember(Order = 6)]
        public string Contact { get; set; }

        [DataMember(Order = 7)]
        public DateTime CreatedDate { get; set; }

        [DataMember(Order = 8)]
        public string CreatedId { get; set; }

        [DataMember(Order = 9)]
        public DateTime UpdatedDate { get; set; }

        [DataMember(Order = 10)]
        public string UpdatedId { get; set; }
    }
}

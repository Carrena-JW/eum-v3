using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data
{
    [DataContract]
    public class SetClientRequest
    {
        [DataMember(Order = 1)]
        public Guid? Id { get; set; }

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
    }
}

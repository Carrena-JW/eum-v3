using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.StepModule.Data
{
    [DataContract]
    public class StepDTO
    {
        [DataMember(Order = 1)]
        public Guid? Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Description { get; set; }

        [DataMember(Order = 4)]
        public bool IsSystem { get; set; }

        [DataMember(Order = 5)]
        public DateTime CreatedDate { get; set; }

        [DataMember(Order = 6)]
        public string CreatedId { get; set; }
    }
}

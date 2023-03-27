using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data
{
    [DataContract]
    public class DelContractRequest
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data
{
    [DataContract]
    public class DelClientRequest
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
    }
}

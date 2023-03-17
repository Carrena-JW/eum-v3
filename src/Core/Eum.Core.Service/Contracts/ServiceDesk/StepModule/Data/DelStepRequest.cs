using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.StepModule.Data
{
    [DataContract]
    public class DelStepRequest
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
    }
}

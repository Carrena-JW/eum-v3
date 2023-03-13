using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.Data.Step.Params
{
    [DataContract]
    public class SetStepRequest
    {
        [DataMember(Order = 1)]
        public Step Item { get; set; }
    }
}

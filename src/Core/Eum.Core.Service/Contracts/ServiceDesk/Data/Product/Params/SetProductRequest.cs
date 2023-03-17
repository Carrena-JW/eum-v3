using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.ServiceDesk.Product.Params
{
    [DataContract]
    public class SetProductRequest
    {
        [DataMember(Order = 1)]
        public ProductDTO Item { get; set; }
    }
}

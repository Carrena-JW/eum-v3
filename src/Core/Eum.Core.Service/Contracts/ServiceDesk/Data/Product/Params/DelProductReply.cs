using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.Product.Params
{
    [DataContract]
    public class DelProductReply
    {
        [DataMember(Order = 1)]
        public bool Succeed { get; set; }
    }
}

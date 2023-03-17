using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ProductModule.Data
{
    [DataContract]
    public class DelProductReply
    {
        [DataMember(Order = 1)]
        public bool Succeed { get; set; }
    }
}

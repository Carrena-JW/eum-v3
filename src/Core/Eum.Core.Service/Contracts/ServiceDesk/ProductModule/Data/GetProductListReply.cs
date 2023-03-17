using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ProductModule.Data
{
    [DataContract]
    public class GetProductListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<ProductDTO> Items { get; set; }
    }
}

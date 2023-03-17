using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.Product.Params
{
    [DataContract]
    public class GetProductListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<ProductDTO> Items { get; set; }
    }
}

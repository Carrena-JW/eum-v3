using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ProductModule.Data
{
    /// <summary>
    /// 제품 등록 및 업데이트에 대한 결과 계약
    /// </summary>
    [DataContract]
    public class SetProductReply
    {
        /// <summary>
        /// 등록 및 업데이트된 데이터
        /// </summary>
        [DataMember(Order = 1)]
        public ProductDTO? Item { get; set; }
    }
}

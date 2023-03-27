using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data
{
    [DataContract]
    public class GetContractListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<ContractDTO> Items { get; set; }
    }
}

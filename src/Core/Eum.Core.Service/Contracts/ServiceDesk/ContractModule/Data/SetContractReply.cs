using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data
{
    [DataContract]
    public class SetContractReply
    {
        [DataMember(Order = 1)]
        public ContractDTO Item { get; set; }
    }
}

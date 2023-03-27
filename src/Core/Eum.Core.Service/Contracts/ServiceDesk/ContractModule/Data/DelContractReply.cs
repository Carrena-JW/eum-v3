using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ContractModule.Data
{
    [DataContract]
    public class DelContractReply
    {
        [DataMember(Order = 1)]
        public bool Succeed { get; set; }
    }
}

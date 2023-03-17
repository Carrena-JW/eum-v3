using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.CaseModule
{
    [DataContract]
    public class CaseInfo
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
    }
}

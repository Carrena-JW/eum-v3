using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.CompanyModule.Data
{
    [DataContract]
    public class DelCompanyReply
    {
        [DataMember(Order = 1)]
        public bool Succeed { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.CompanyModule.Data
{
    [DataContract]
    public class GetCompanyListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<CompanyDTO> Items { get; set; }
    }
}

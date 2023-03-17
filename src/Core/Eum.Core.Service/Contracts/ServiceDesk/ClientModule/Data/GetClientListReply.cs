using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data
{
    [DataContract]
    public class GetClientListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<ClientDTO> Items { get; set; }
    }
}

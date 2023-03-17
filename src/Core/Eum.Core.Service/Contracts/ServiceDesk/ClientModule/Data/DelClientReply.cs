using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data
{
    [DataContract]
    public class DelClientReply
    {
        [DataMember(Order = 1)]
        public bool Succeed { get; set; }
    }
}

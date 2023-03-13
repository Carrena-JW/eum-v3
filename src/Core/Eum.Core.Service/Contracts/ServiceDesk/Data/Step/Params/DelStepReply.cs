using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.Data.Step.Params
{
    [DataContract]
    public class DelStepReply
    {
        [DataMember(Order = 1)]
        public bool Succeed { get; set; }
    }
}

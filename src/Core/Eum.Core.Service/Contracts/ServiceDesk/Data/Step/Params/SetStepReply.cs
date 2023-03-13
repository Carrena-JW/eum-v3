using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.Data.Step.Params
{
    [DataContract]
    public class SetStepReply
    {
        [DataMember(Order = 1)]
        public Step Item { get; set; }
    }
}

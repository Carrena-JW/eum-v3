using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.StepModule.Data
{
    [DataContract]
    public class SetStepReply
    {
        [DataMember(Order = 1)]
        public StepDTO Item { get; set; }
    }
}

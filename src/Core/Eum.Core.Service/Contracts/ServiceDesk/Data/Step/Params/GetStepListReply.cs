using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.Data.Step.Params
{
    [DataContract]
    public class GetStepListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<Step> Items { get; set; }
    }
}

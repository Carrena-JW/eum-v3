﻿using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.ServiceDesk.StepModule.Data
{
    [DataContract]
    public class GetStepListReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<StepDTO> Items { get; set; }
    }
}
﻿using System.Runtime.Serialization;

namespace Eum.ServiceClient.Contracts.ServiceDesk.Data.Case
{
    [DataContract]
    public class CaseInfo
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
    }
}

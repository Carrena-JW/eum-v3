using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Account.TokenModule.Data
{
    [DataContract]
    public class TokenRequest
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; } = string.Empty;
    }
}

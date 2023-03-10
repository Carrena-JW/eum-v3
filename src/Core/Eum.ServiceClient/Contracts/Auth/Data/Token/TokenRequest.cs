using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.ServiceClient.Contracts.Auth.Data.Token
{
    [DataContract]
    public class TokenRequest
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.ServiceClient.Contracts.Auth.Data.Auth
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; } = string.Empty;

        [DataMember(Order = 2)]
        public string Password { get; set; } = String.Empty;
    }
}

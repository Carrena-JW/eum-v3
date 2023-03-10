using Eum.ServiceClient.Contracts.Auth.Data.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eum.ServiceClient.Contracts.Auth.Data.Auth
{
    [DataContract]
    public class SignInReply
    {
        [DataMember(Order = 1)]
        public TokenReply Token { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }

        [DataMember(Order = 3)]
        public IEnumerable<string> Errors { get; set; }
    }
}

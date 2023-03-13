using Eum.Core.Service.Contracts.Auth.Data.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Account.Data.Auth
{
    [DataContract]
    public class SignInReply
    {
        [DataMember(Order = 1)]
        public string Token { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }

        [DataMember(Order = 3)]
        public IEnumerable<string> Errors { get; set; }
    }
}

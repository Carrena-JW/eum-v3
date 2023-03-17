using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Account.AuthModule.Data
{
    [DataContract]
    public class SignInReply
    {
        [DataMember(Order = 1)]
        public string Token { get; set; }

        [DataMember(Order = 2)]
        public bool Succeed { get; set; }

        [DataMember(Order = 3)]
        public IEnumerable<string> Errors { get; set; }
    }
}

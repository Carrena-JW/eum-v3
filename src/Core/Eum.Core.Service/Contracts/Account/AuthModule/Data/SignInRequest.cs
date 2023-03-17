using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Account.AuthModule.Data
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; } = string.Empty;

        [DataMember(Order = 2)]
        public string Password { get; set; } = string.Empty;
    }
}

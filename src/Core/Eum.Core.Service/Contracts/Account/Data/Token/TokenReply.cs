using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Auth.Data.Token
{
    [DataContract]
    public class TokenReply
    {
        [DataMember(Order = 1)]
        public string AccessToken { get; set; } = string.Empty;

        [DataMember(Order = 2)]
        public string RefreshToken { get; set; } = string.Empty;
    }
}

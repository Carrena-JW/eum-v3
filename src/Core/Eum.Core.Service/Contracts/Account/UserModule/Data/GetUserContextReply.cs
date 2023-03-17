using Eum.Core.Shared.Models;
using System.Runtime.Serialization;

namespace Eum.Core.Service.Contracts.Account.UserModule.Data
{
    [DataContract]
    public class GetUserContextReply
    {
        [DataMember(Order = 1)]
        public Person User { get; set; }
    }
}

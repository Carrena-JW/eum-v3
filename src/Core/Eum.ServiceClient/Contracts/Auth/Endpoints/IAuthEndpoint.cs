using Eum.ServiceClient.Contracts.Auth.Data.Auth;
using Eum.ServiceClient.Contracts.Auth.Data.Token;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eum.ServiceClient.Contracts.Auth.Endpoints
{
    [ServiceContract]
    public interface IAuthEndpoint
    {
        [OperationContract]
        Task<SignInReply> SignInAsync(SignInRequest request, CallContext context = default);
    }
}

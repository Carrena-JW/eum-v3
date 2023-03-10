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
    public interface ITokenEndpoint
    {
        [OperationContract]
        Task<TokenReply> CreateAsync(TokenRequest request, CallContext context = default);
    }
}

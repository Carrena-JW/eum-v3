using Eum.Core.Service.Contracts.Account.Data.User;
using Eum.Core.Service.Contracts.Auth;
using Eum.Core.Service.Contracts.Auth.Data.Token;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using System.Net;
using System.ServiceModel;

namespace Eum.Core.Service.Contracts.Account.Endpoints
{
    [ServiceContract]
    public interface IUserEndpoint : IEndpoint
    {
        [OperationContract]
        Task<GetUserContextReply> GetUserContextAsync(GetUserContextRequest request, CallContext context = default);
    }

    [Area("Account")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private IAccountClient _accountClient;

        public UserController(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }

        [HttpGet("Context")]
        public async Task<GetUserContextReply> GetUserContext()
        {
            var result = await _accountClient.User.GetUserContextAsync(new GetUserContextRequest());
            return result;
        }
    }
}

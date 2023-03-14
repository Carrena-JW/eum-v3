using Eum.Core.Service.Contracts.Auth.Data.Token;
using Eum.Core.Shared.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Eum.Core.Shared.Identity.JwtAuth;
using Eum.Core.Service.Contracts.Auth;
using Eum.Core.Service.Contracts.Account.Data.Auth;

namespace Eum.Core.Service.Contracts.Account.Endpoints
{

    [ServiceContract]
    public interface IAuthEndpoint : IEndpoint
    {
        [OperationContract]
        Task<SignInReply> SignInAsync(SignInRequest request, CallContext context = default);
    }

    [Area("Account")]
    [Route("[Area]/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAccountClient _authClient;
        private IAppConfigRepository _appConfigRepository;
        private IConfiguration _configuration;

        public AuthController(IAccountClient authClient, IAppConfigRepository appConfigRepository, IConfiguration configuration)
        {
            _authClient = authClient;
            _appConfigRepository = appConfigRepository;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<SignInReply> SignIn(SignInRequest request)
        {
            var result = await _authClient.Auth.SignInAsync(request);
            if (result.Success)
                Response.SetCookie(result.Token);
            return result;
        }

        [HttpGet("[action]")]
        [Authorize]
        public Task<string> GetUserName()
        {
            return Task.FromResult(HttpContext.User.GetPersonCode());
        }
    }
}

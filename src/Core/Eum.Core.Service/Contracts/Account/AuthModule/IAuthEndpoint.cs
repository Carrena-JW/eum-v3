using Eum.Core.Shared.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Eum.Core.Service.Contracts.Auth;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using Eum.Core.Service.Contracts.Account.AuthModule.Data;

namespace Eum.Core.Service.Contracts.Account.AuthModule
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
        private IAccountClient _accountClient;
        private IAppConfigRepository _appConfigRepository;
        private IConfiguration _configuration;

        public AuthController(IAccountClient accountClient, IAppConfigRepository appConfigRepository, IConfiguration configuration)
        {
            _accountClient = accountClient;
            _appConfigRepository = appConfigRepository;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<SignInReply> SignIn(SignInRequest request)
        {
            var result = await _accountClient.Auth.SignInAsync(request);
            if (result.Succeed)
            {
                Response.SetCookie(result.Token);
            }
            return result;
        }
    }
}

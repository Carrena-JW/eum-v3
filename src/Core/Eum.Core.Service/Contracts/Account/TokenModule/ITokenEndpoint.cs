using Eum.Core.Service.Contracts.Account.TokenModule.Data;
using Eum.Core.Shared.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Account.TokenModule
{
    [ServiceContract]
    public interface ITokenEndpoint : IEndpoint
    {
        [OperationContract]
        Task<TokenReply> CreateAsync(TokenRequest request, CallContext context = default);
    }

    [Area("Account")]
    [Route("[Area]/[controller]")]
    [Authorize]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpGet("Current")]
        [Authorize]
        public Task<string> GetToken()
        {
            var token = HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", "");
            return Task.FromResult(token);
        }
    }
}

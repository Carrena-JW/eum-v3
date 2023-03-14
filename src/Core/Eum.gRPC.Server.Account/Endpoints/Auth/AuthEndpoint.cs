using Eum.Core.Shared.Services;
using Eum.Core.Service.Contracts.Auth.Data.Token;
using ProtoBuf.Grpc;
using Microsoft.AspNetCore.Authorization;
using Eum.Core.Service.Contracts.Account.Endpoints;
using Eum.Core.Service.Contracts.Account.Data.Auth;
using Eum.Core.Shared.Exceptions;

namespace Eum.gRPC.Server.Account.Endpoints.Auth
{
    public class AuthEndpoint : IAuthEndpoint
    {
        private IPersonService _personService;
        private ITokenService _tokenService;
        private IHashService _hashService;

        public AuthEndpoint(IPersonService personService, ITokenService tokenService, IHashService hashService)
        {
            _personService = personService;
            _tokenService = tokenService;
            _hashService = hashService;
        }

        [AllowAnonymous]
        public async Task<SignInReply> SignInAsync(SignInRequest request, CallContext context = default)
        {
            var inputUserName = request.UserName;
            var inputPassword = request.Password;
            var errors = new List<string>();
            var reply = new SignInReply();

            if (string.IsNullOrWhiteSpace(inputUserName)) errors.Add("User ID is required.");
            if (string.IsNullOrWhiteSpace(inputPassword)) errors.Add("Password is required.");
            try
            {
                var user = _personService.GetPerson(inputUserName);
                var hashPassword = _hashService.GetHashString(inputPassword);
                if (_hashService.IsMatchTwoHashString(hashPassword, user.Password))
                {
                    var token = await _tokenService.CreateEumCookieValue(inputUserName);
                    reply.Token = token;
                }
                else
                {
                    errors.Add("Password does not match");
                }
            }
            catch(UserNotFoundException)
            {
                errors.Add("User does not exist");
            }

            reply.Errors = errors;
            reply.Success = errors.Count() == 0;
            return reply;
        }
    }
}

using Eum.Core.Service.Contracts.Account.AuthModule;
using Eum.Core.Service.Contracts.Account.TokenModule;
using Eum.Core.Service.Contracts.Account.UserModule;
using Microsoft.AspNetCore.Http;

namespace Eum.Core.Service.Contracts.Auth
{
    /// <summary>
    /// 계정 클라이언트 인터페이스
    /// </summary>
    public interface IAccountClient
    {
        /// <summary>
        /// 인증 관리
        /// </summary>
        IAuthEndpoint Auth { get; }
        /// <summary>
        /// 토큰 관리
        /// </summary>
        ITokenEndpoint Token { get; }
        /// <summary>
        /// 사용자 관리
        /// </summary>
        IUserEndpoint User { get; }
    }

    /// <summary>
    /// 계정 클라이언트 인터페이스
    /// </summary>
    public class AccountClient : GrpcClientBase, IAccountClient
    {
        /// <summary>
        /// 인증 관리
        /// </summary>
        public IAuthEndpoint Auth { get; }
        /// <summary>
        /// 토큰 관리
        /// </summary>
        public ITokenEndpoint Token { get; }
        /// <summary>
        /// 사용자 관리
        /// </summary>
        public IUserEndpoint User { get; }

        /// <summary>
        /// 계정 관리 클라이언트 생성자
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public AccountClient(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            Auth = CreateClient<IAuthEndpoint>(token);
            User = CreateClient<IUserEndpoint>(token);
            Token = CreateClient<ITokenEndpoint>(token);
        }

        /// <summary>
        /// 서버 연결 정보를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        protected override string GetAddress()
        {
            return "https://localhost:7073";
        }
    }
}

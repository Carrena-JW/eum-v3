using Eum.gRPC.Shared.Helpders;
using Grpc.Core;
using static Eum.gRPC.Server.Mail.Mail;

namespace Eum.gRPC.Server.Mail.Endpoints
{
    public class MailEndpoints : MailBase
    {
        private readonly ILogger<MailEndpoints> _logger;
        public MailEndpoints(ILogger<MailEndpoints> logger)
        {
            _logger = logger;
        }

        public override Task<MailReplay> GetMailList(MailRequest request, ServerCallContext context)
        {
            _logger.LogInformation("[GetMailList] recived message");
            //조회전 common rpac 에서 인증 확인 하는 코드
            // 예를 들어 타 서비스 호출이 필요한 경우를 예로 듬
            // 

            // DI 구현 생략
            var client = new gRPCClientService(Shared.Enums.EnumEndpointTarget.Common);

            var response = client.IsLoginUser();

            return Task.FromResult(new MailReplay
            {
                Message = "this is mail list response."
            });
        }
    }
}
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
            //��ȸ�� common rpac ���� ���� Ȯ�� �ϴ� �ڵ�
            // ���� ��� Ÿ ���� ȣ���� �ʿ��� ��츦 ���� ��
            // 

            // DI ���� ����
            var client = new gRPCClientService(Shared.Enums.EnumEndpointTarget.Common);

            var response = client.IsLoginUser();

            return Task.FromResult(new MailReplay
            {
                Message = "this is mail list response."
            });
        }
    }
}
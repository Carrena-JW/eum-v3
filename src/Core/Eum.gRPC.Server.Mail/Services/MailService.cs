using Eum.gRPC.Server.Proto.Mail;
using Grpc.Core;
using static Eum.gRPC.Server.Proto.Mail.Mail;

namespace Eum.gRPC.Server.Mail.Services
{
    public class MailService : MailBase
    {
        private readonly ILogger<MailService> _logger;
        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public override Task<MailReplay> GetMailList(MailRequest request, ServerCallContext context)
        {
            return Task.FromResult(new MailReplay
            {
                Message = "this is mail list response."
            });
        }
    }
}
using Eum.gRPC.Server.Proto.Contact;
using Grpc.Core;
using static Eum.gRPC.Server.Proto.Contact.Contact;

namespace Eum.gRPC.Server.Mail.Services
{
    public class ContactService : ContactBase
    {
        private readonly ILogger<MailService> _logger;
        public ContactService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public override Task<ContactReplay> GetContactList(ContactRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ContactReplay
            {
                Message = "this is contact list response."
            });
        }
    }
}
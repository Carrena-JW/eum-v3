using Grpc.Core;
using static Eum.gRPC.Server.Mail.Contact;

namespace Eum.gRPC.Server.Mail.Endpoints
{
    public class ContactEndpoint : ContactBase
    {
        private readonly ILogger<ContactEndpoint> _logger;
        public ContactEndpoint(ILogger<ContactEndpoint> logger)
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
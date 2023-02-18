using Eum.gRPC.Server.Proto.Calendar;
using Grpc.Core;
using static Eum.gRPC.Server.Proto.Calendar.Calendar;

namespace Eum.gRPC.Server.Mail.Services
{
    public class CalendarService : CalendarBase
    {
        private readonly ILogger<MailService> _logger;
        public CalendarService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public override Task<CalendarReplay> GetCalendarList(CalendarRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalendarReplay
            {
                Message = "this is calendar list response."
            });
        }
    }
}
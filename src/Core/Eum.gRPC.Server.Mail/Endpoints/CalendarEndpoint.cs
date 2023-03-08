using Grpc.Core;
using static Eum.gRPC.Server.Mail.Calendar;

namespace Eum.gRPC.Server.Mail.Endpoints
{
    public class CalendarEndpoint : CalendarBase
    {
        private readonly ILogger<CalendarEndpoint> _logger;
        public CalendarEndpoint(ILogger<CalendarEndpoint> logger)
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
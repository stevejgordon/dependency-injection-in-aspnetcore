namespace TennisBookings.Services.Time
{
	public class TimeService : ITimeService, IUtcTimeService
	{
		//private readonly ILogger<TimeService> _logger;

		//public TimeService(ILogger<TimeService> logger)
		//{
		//	_logger = logger;
		//}

		//public TimeService()
		//{
		//	var guid = Guid.NewGuid();



		//	_logger.LogInformation($"TimeService initialised: {guid}");
		//}

		public DateTime CurrentTime => DateTime.Now;

		public DateTime CurrentUtcDateTime => DateTime.UtcNow;
	}
}

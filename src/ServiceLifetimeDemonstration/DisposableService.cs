namespace ServiceLifetimeDemonstration
{
	public class DisposableService : IDisposable
	{
		private readonly ILogger<DisposableService> _logger;

		public DisposableService(ILogger<DisposableService> logger)
		{
			_logger = logger;
		}

		public void Dispose()
		{
			_logger.LogInformation("DISPOSING OF SERVICE");
		}
	}
}

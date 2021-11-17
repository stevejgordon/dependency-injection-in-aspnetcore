namespace TennisBookings.Services.Courts
{
	public class CourtMaintenanceService : ICourtMaintenanceService
{
		private readonly TennisBookingsDbContext _dbContext;

		public CourtMaintenanceService(TennisBookingsDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<CourtMaintenanceSchedule>> GetUpcomingMaintenance()
		{
			return await _dbContext.CourtMaintenance!
				.AsNoTracking()
				.Include(x => x.Court)
				.Where(x => x.EndDate > DateTime.Now).ToListAsync();
		}
	}
}

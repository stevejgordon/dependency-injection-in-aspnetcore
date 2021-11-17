using Microsoft.EntityFrameworkCore;

namespace TennisBookings.Services.Bookings
{
	public class CourtBookingService : ICourtBookingService
{
		private readonly TennisBookingsDbContext _dbContext;
		private readonly IUtcTimeService _utcTimeService = new TimeService();

		public CourtBookingService(TennisBookingsDbContext dbContext/*, IUtcTimeService utcTimeService*/)
		{
			_dbContext = dbContext;
			//_utcTimeService = utcTimeService;
		}

		public async Task CreateCourtBooking(CourtBooking courtBooking)
		{
			_dbContext.CourtBookings!.Add(courtBooking);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<CourtBooking?> LoadBooking(int bookingId)
		{
			var booking = await _dbContext.CourtBookings!
				.AsNoTracking()
				.Include(x => x.Court)
				.Include(x => x.Member)
				.SingleOrDefaultAsync(x => x.Id == bookingId);

			return booking;
		}

		public async Task<bool> CancelBooking(int bookingId)
		{
			var booking = await _dbContext.CourtBookings!.FindAsync(bookingId);

			if (booking == null)
				return false;

			_dbContext.CourtBookings.Remove(booking);

			await _dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<CourtBooking>> BookingsUntilDateAsync(DateTime date)
		{
			var bookings = await _dbContext.CourtBookings!
				.AsNoTracking()
				.Include(x => x.Court)
				.Include(x => x.Member)
				.Where(x => x.StartDateTime >= _utcTimeService.CurrentUtcDateTime && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1))
				.ToListAsync();

			return bookings;
		}

		public async Task<IEnumerable<CourtBooking>> BookingsForDayAsync(DateTime date)
		{
			var bookings = await _dbContext.CourtBookings!
				.AsNoTracking()
				.Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1))
				.ToListAsync();

			return bookings;
		}

		public async Task<IEnumerable<CourtBooking>> CourtBookingsForDayAsync(DateTime date, int courtId)
		{
			var bookings = await _dbContext.CourtBookings!
				.AsNoTracking()
				.Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1) && x.CourtId == courtId)
				.ToListAsync();

			return bookings;
		}

		public async Task<IEnumerable<CourtBooking>> MemberBookingsForDayAsync(DateTime date, Member member)
		{
			var bookings = await _dbContext.CourtBookings!
				.AsNoTracking()
				.Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1) && x.Member == member)
				.ToListAsync();

			return bookings;
		}

		public async Task<IEnumerable<CourtBooking>> GetFutureBookingsForMemberAsync(Member member)
		{
			return await _dbContext.CourtBookings!
				.AsNoTracking()
				.Where(c => c.Member == member && c.StartDateTime >= DateTimeOffset.UtcNow)
				.OrderBy(x => x.StartDateTime)
				.ToListAsync();
		}

		public async Task<int> GetBookedHoursForMemberAsync(int memberId, DateTime date)
		{
			var member = await _dbContext.Members!.FindAsync(memberId);

			if (member == null)
				throw new Exception("Member not found"); // should have better error handling here

			return await GetBookedHoursForMemberAsync(member, date);
		}

		public async Task<int> GetBookedHoursForMemberAsync(Member member, DateTime date)
		{
			var bookings = await _dbContext.CourtBookings!
				.AsNoTracking()
				.Where(c => c.Member == member && c.StartDateTime >= date.Date && c.EndDateTime <= date.Date.AddDays(1).AddMilliseconds(-1))
				.ToListAsync();

			var hoursBooked = 0;

			foreach (var booking in bookings)
			{
				var length = (booking.EndDateTime - booking.StartDateTime).Hours;
				hoursBooked += length;
			}

			return hoursBooked;
		}
	}
}

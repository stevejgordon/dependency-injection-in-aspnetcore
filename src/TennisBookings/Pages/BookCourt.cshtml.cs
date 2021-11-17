using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace TennisBookings.Pages
{
	public class BookCourtModel : PageModel
    {
        private readonly ICourtBookingManager _courtBookingManager;
        private readonly UserManager<TennisBookingsUser> _userManager;
        private readonly IBookingService _bookingService;
        private readonly BookingConfiguration _bookingConfiguration;

        public BookCourtModel(ICourtBookingManager courtBookingManager, UserManager<TennisBookingsUser> userManager, IOptions<BookingConfiguration> bookingConfig, IBookingService bookingService)
        {
            _courtBookingManager = courtBookingManager;
            _userManager = userManager;
            _bookingService = bookingService;
            _bookingConfiguration = bookingConfig.Value;
        }

        public string[] Errors { get; set; } = Array.Empty<string>();

        [BindProperty(SupportsGet = true)]
        public DateTime BookingStartTime { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CourtId { get; set; }

        [DisplayName("Booking Length (Hours)")]
        [BindProperty]
        public int BookingLengthInHours { get; set; }

        public SelectList PossibleHourLengths { get; set; } = new SelectList(Array.Empty<int>());

        public async Task OnGet()
        {
            var maxHours = _bookingConfiguration.MaxRegularBookingLengthInHours;

            var maxAvailableHour =
                await _bookingService.GetMaxBookingSlotForCourtAsync(BookingStartTime, BookingStartTime.AddHours(maxHours),
                    CourtId);

            PossibleHourLengths = new SelectList(Enumerable.Range(1, maxAvailableHour));
        }

        public async Task<IActionResult> OnPost()
        {
            if (BookingStartTime < DateTime.UtcNow)
            {
                return new BadRequestResult();
            }

            var user = await _userManager.Users
                .Include(u => u.Member)
                .FirstOrDefaultAsync(u => u.NormalizedEmail == User.Identity.Name);

            if (user == null)
                return new ChallengeResult();
            
            var result = await _courtBookingManager.MakeBookingAsync(BookingStartTime, BookingStartTime.AddHours(BookingLengthInHours), CourtId, user.Member);

            if (result.BookingSuccessful)
            {
                TempData["BookingSuccess"] = true;

                return RedirectToPage("/Bookings");
            }

            Errors = result.Errors.ToArray();
            return Page();
        }
    }
}

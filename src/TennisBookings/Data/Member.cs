﻿namespace TennisBookings.Data
{
    public class Member
    {
        public int Id { get; set; }

        public string? Forename { get; set; }

        public string? Surname { get; set; }
        
        public DateTime JoinDate { get; set; }

		public ICollection<CourtBooking> CourtBookings { get; set; } = Array.Empty<CourtBooking>();

        public string? UserId { get; set; }

        public TennisBookingsUser? User { get; set; }
    }
}

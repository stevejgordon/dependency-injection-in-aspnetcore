using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TennisBookings.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Filename=:memory:", options =>
        //    {
        //        options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        //    });

        //    base.OnConfiguring(optionsBuilder);
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
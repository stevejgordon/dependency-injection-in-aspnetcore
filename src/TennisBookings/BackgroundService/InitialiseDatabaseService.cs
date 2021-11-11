using Microsoft.AspNetCore.Identity;
using TennisBookings.Data;

namespace TennisBookings.BackgroundService
{
    public class InitialiseDatabaseService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public InitialiseDatabaseService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Blocks until this is completed

            using var serviceScope = _serviceProvider.CreateScope();
            
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = new IdentityUser("admin@example.com")
            {
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            var password = new PasswordHasher<IdentityUser>();
            var hashed = password.HashPassword(user, "password");
            user.PasswordHash = hashed;

            await userManager.CreateAsync(user);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

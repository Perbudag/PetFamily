using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure.Database;

namespace PetFamily.API.Extensions
{
    public static class AppExtensions
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PetFamilyDbContext>();

            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.MigrateAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Infrastructure.Database
{
    public class PetFamilyDbContext : DbContext
    {
        private const string CONNECTION_STRING = "Postgres.PetFamily";

        private readonly IConfiguration _configuration;


        public DbSet<Volunteer> Volunteers { get; set; }


        public PetFamilyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var test = _configuration.GetConnectionString(CONNECTION_STRING);
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(CONNECTION_STRING));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetFamilyDbContext).Assembly);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });

    }
}

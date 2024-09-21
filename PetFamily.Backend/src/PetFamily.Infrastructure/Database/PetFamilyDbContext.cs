using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.SpeciesAggregate;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Infrastructure.Database
{
    public class PetFamilyDbContext : DbContext
    {
        private const string CONNECTION_STRING = "Postgres.PetFamily";

        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;


        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Species> Species { get; set; }


        public PetFamilyDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var test = _configuration.GetConnectionString(CONNECTION_STRING);
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(CONNECTION_STRING));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetFamilyDbContext).Assembly);
        }

    }
}

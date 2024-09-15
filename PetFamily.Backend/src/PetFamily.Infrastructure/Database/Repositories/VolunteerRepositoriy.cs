using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Interfaces.Repositories;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Infrastructure.Database.Repositories
{
    internal class VolunteerRepositoriy : IVolunteerRepositoriy
    {
        private readonly PetFamilyDbContext _context;

        public VolunteerRepositoriy(PetFamilyDbContext context)
        {
            _context = context;
        }
        

        public async Task<bool> EmailExists(EmailAddress address, CancellationToken cancellationToken = default)
        {
            var result = await _context.Volunteers.FirstOrDefaultAsync(v => v.Email == address);
            return result is not null;
        }

        public async Task<bool> PhoneNumberExists(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            var result = await _context.Volunteers.FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber);

            return result is not null;
        }
    }
}

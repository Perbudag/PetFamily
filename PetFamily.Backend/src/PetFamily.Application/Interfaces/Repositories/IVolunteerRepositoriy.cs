using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Interfaces.Repositories
{
    public interface IVolunteerRepositoriy
    {
        public Task<bool> EmailExists(EmailAddress address, CancellationToken cancellationToken = default);
        public Task<bool> PhoneNumberExists(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
    }
}

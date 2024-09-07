using PetFamily.Domain.VolunteerAggregate.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetFamily.Application.Interfaces.Repositories
{
    public interface IVolunteerRepositoriy
    {
        public Task<bool> EmailExists(EmailAddress address, CancellationToken cancellationToken = default);
        public Task<bool> PhoneNumberExists(string phoneNumber, CancellationToken cancellationToken = default);
    }
}

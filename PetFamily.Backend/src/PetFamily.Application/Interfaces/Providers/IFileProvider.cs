using PetFamily.Application.Models;
using PetFamily.Application.Tools;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.Application.Interfaces.Providers
{
    public interface IFileProvider
    {
        BatchTask<Result<string>> Upload(List<FileData> filesData, CancellationToken cancellationToken = default);

        Task<Result<string>> GetLink(FileLocation fileLocation);

        Task<Result> Remove(FileLocation fileLocation, CancellationToken cancellationToken = default);
    }
}

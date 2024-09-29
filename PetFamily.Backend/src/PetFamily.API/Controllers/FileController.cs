using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Models;
using PetFamily.Application.Interfaces.Providers;
using PetFamily.Application.Models;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<string>>> UploadFile(
            [FromServices] ILogger<FileController> logger,
            [FromServices] IFileProvider fileProvider,
            [FromForm] IFormFileCollection formFileCollection,
            CancellationToken cancellationToken)
        {
            await using FileDataList filesData = new(formFileCollection);

            List<Error> errors = new();
            Result<List<string>> result = new List<string>();

            var batchTask = await fileProvider.Upload(filesData, cancellationToken);

            batchTask.OnExecuted(fileName => 
            {
                if(fileName.IsSuccess)
                {
                    logger.LogInformation("End uploading file with new name: {newName}", fileName);

                    result.Value.Add(fileName.Value);
                }
                else
                {
                    errors.AddRange(fileName.Errors);
                }
            });

            await batchTask.Run();

            if (errors.Count > 0)
                return Result.Failure(errors).ToResponse();

            return result.ToResponse();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetFilesLink(
            [FromServices] IFileProvider fileProvider,
            [FromQuery] IEnumerable<string> fileNames,
            CancellationToken cancellationToken)
        {
            List<FileLocation> filesLocation = new();

            foreach (var fileName in fileNames)
                filesLocation.Add(new FileLocation("test", fileName));

            List<Error> errors = new();
            List<string> filePaths = new();

            foreach (var fileLocation in filesLocation)
            {
                var getFileResult = await fileProvider.GetLink(fileLocation);

                if (getFileResult.IsFailure)
                {
                    errors.AddRange(getFileResult.Errors);
                }
                else
                {
                    filePaths.Add(getFileResult.Value);
                }
            }

            if (errors.Count > 0)
                return Result.Failure(errors).ToResponse();

            Result<IEnumerable<string>> result = filePaths;

            return result.ToResponse();
        }


        [HttpDelete("{fileName}")]
        public async Task<ActionResult> DeleteFile(
            [FromServices] ILogger<FileController> logger,
            [FromServices] IFileProvider fileProvider,
            [FromRoute] string fileName,
            CancellationToken cancellationToken)
        {
            var fileLocation = new FileLocation("test", fileName);

            var result = await fileProvider.Remove(fileLocation, cancellationToken);

            return result.ToResponse();
        }
    }
}

using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PetFamily.Application.Interfaces.Providers;
using PetFamily.Application.Models;
using PetFamily.Application.Tools;
using PetFamily.Domain.Shared.Models;

namespace PetFamily.Infrastructure.Providers
{
    internal class MinioProvider : IFileProvider
    {
        private const int MAX_DEGREE_OF_PARALLELISM = 3;

        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<BatchTask<string>>> Upload(List<FileData> filesData, CancellationToken cancellationToken = default)
        {
            List<Func<string>> funcs = new();

            foreach (var fileData in filesData)
            {
                await CreateButcketIfNotExists(fileData.Location.BucketName, cancellationToken);

                funcs.Add(() => PutFile(fileData, cancellationToken).Result);
            }

            return new BatchTask<string>(funcs, MAX_DEGREE_OF_PARALLELISM);
        }

        public async Task<Result<string>> GetLink(FileLocation fileLocation)
        {
            try
            {
                var statArgs = new StatObjectArgs()
                    .WithBucket(fileLocation.BucketName)
                    .WithObject(fileLocation.FileName);

                var statFile = await _minioClient.StatObjectAsync(statArgs);

                if (statFile.ContentType == null)
                {
                    _logger.LogError(
                        "File with bucket \"{BucketName}\", and name \"{FileName}\" not found",
                        fileLocation.BucketName,
                        fileLocation.FileName);
                    return Errors.FileProvider.NotFound(fileLocation.FileName);
                }

                var presignedGetArgs = new PresignedGetObjectArgs()
                    .WithBucket(fileLocation.BucketName)
                    .WithObject(fileLocation.FileName)
                    .WithExpiry(604800);

                var filePath = await _minioClient.PresignedGetObjectAsync(presignedGetArgs);

                if(filePath is null)
                {
                    _logger.LogError(
                        "File with bucket \"{BucketName}\", and name \"{FileName}\" not found",
                        fileLocation.BucketName,
                        fileLocation.FileName);
                    return Errors.FileProvider.NotFound(fileLocation.FileName);
                }

                return filePath;
            }
            catch (MinioException ex)
            {
                _logger.LogError(ex, ex.Message);
                return Errors.FileProvider.NotFound(fileLocation.FileName);
            }
        }

        public async Task<Result> Remove(FileLocation fileLocation, CancellationToken cancellationToken = default)
        {
            try
            {
                var statArgs = new StatObjectArgs()
                    .WithBucket(fileLocation.BucketName)
                    .WithObject(fileLocation.FileName);

                var statFile = await _minioClient.StatObjectAsync(statArgs);

                if (statFile.ContentType == null)
                {
                    _logger.LogError(
                         "File with bucket \"{BucketName}\", and name \"{FileName}\" not found",
                         fileLocation.BucketName,
                         fileLocation.FileName);
                    return Errors.FileProvider.NotFound(fileLocation.FileName);
                }

                var removeArgs = new RemoveObjectArgs()
                    .WithBucket(fileLocation.BucketName)
                    .WithObject(fileLocation.FileName);

                await _minioClient.RemoveObjectAsync(removeArgs);

            }
            catch (MinioException ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Result.Success();
        }


        private async Task CreateButcketIfNotExists(string BucketName, CancellationToken cancellationToken)
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(BucketName);

            bool bucketExist = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(BucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }

        private async Task<string> PutFile(FileData fileData, CancellationToken cancellationToken = default)
        {
            Guid guid = Guid.NewGuid();

            string extension = Path.GetExtension(fileData.Location.FileName);

            string fileName = guid.ToString() + extension;

            var args = new PutObjectArgs()
                .WithBucket(fileData.Location.BucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream!.Length)
                .WithObject(fileName);

            _logger.LogInformation("Uploading a file with name: \"{name}\" (new name: \"{newName}\")", fileData.Location.FileName, fileName);
            var result = await _minioClient.PutObjectAsync(args, cancellationToken);

            return result.ObjectName;
        }
    }
}

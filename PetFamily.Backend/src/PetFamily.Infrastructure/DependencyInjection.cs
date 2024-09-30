using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Commands.Volunteer.Delete;
using PetFamily.Application.Commands.Volunteer.Update;
using PetFamily.Application.Commands.Volunteer.UpdateRequisites;
using PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks;
using PetFamily.Application.Interfaces.Providers;
using PetFamily.Application.Interfaces.Repositories;
using PetFamily.Infrastructure.CommandHandlers.VolunteerCommands;
using PetFamily.Infrastructure.Database;
using PetFamily.Infrastructure.Database.Repositories;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;

namespace PetFamily.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddCommands();
            services.AddRepositories();
            services.AddFileProvider(configuration);

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<IVolunteerCreateHandler, VolunteerCreateHandler>();
            services.AddScoped<IVolunteerUpdateMainInfoHandler, VolunteerUpdateMainInfoHandler>();
            services.AddScoped<IVolunteerUpdateRequisitesHandler, VolunteerUpdateRequisitesHandler>();
            services.AddScoped<IVolunteerUpdateSocialNetworksHandler, VolunteerUpdateSocialNetworksHandler>();
            services.AddScoped<IVolunteerDeleteHandler, VolunteerDeleteHandler>();

            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddScoped<PetFamilyDbContext>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVolunteerRepositoriy, VolunteerRepositoriy>();

            return services;
        }

        public static IServiceCollection AddFileProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));

            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                                   ?? throw new ApplicationException("Missing minio configuration");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSsl);
            });

            services.AddScoped<IFileProvider, MinioProvider>();

            return services;
        }
    }
}

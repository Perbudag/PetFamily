using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Commands.Volunteer.Create;
using PetFamily.Application.Commands.Volunteer.Update;
using PetFamily.Application.Commands.Volunteer.UpdateRequisites;
using PetFamily.Application.Commands.Volunteer.UpdateSocialNetworks;
using PetFamily.Application.Interfaces.Repositories;
using PetFamily.Infrastructure.CommandHandlers.VolunteerCommands;
using PetFamily.Infrastructure.Database;
using PetFamily.Infrastructure.Database.Repositories;

namespace PetFamily.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<PetFamilyDbContext>();

            services.AddCommands();
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<ICreateVolunteerHandler, CreateVolunteerHandler>();
            services.AddScoped<IUpdateVolunteerMainInfoHandler, UpdateVolunteerMainInfoHandler>();
            services.AddScoped<IUpdateRequisitesHandler, UpdateRequisitesHandler>();
            services.AddScoped<IUpdateSocialNetworksHandler, UpdateSocialNetworksHandler>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVolunteerRepositoriy, VolunteerRepositoriy>();

            return services;
        }
    }
}

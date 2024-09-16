﻿using Serilog.Events;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PetFamily.API.Validation;

namespace PetFamily.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddFluentValidation(configuration);
            services.AddSerilog(configuration);

            return services;
        }

        private static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Seq(configuration.GetConnectionString("Seq")
                             ?? throw new ArgumentNullException("Seq"))
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .CreateLogger();

            services.AddSerilog();

            return services;
        }

        private static IServiceCollection AddFluentValidation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation(cfg =>
            {
                cfg.OverrideDefaultResultFactoryWith<CustomResultFactory>();
            });

            return services;
        }
    }
}

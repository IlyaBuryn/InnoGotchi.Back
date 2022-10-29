using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.DataAccess.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.BusinessLogic.Components
{
    public static class ConfigurationBusinessLogicModule
    {
        private static void ConfigurationManagers(IServiceCollection service)
        {
            service.AddScoped<IPetService, PetService>();
            service.AddScoped<IFarmService, FarmService>();
            service.AddScoped<IIdentityService, IdentityService>();
            service.AddScoped<IVitalSignService, VitalSignService>();
        }

        public static void ConfigurationBusinessLogicManagers(this IServiceCollection builder, string connectionString)
        {
            builder.ConfigurationDataAccessServices(connectionString);
            builder.AddDataValidation();
            ConfigurationManagers(builder);
        }

        public static void Configure(this IApplicationBuilder app)
        {
            app.ConfigureDataAccessLayer();
        }
    }
}

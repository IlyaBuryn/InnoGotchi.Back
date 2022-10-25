using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.DataAccess.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.BusinessLogic.Components
{
    public static class ConfigurationBusinessLogicModule
    {
        private static void ConfigurationManagers(IServiceCollection service)
        {
            service.AddScoped<IPetService, PetService>();
            service.AddScoped<IFarmService, FarmService>();
            service.AddScoped<IAccountService, AccountService>();
        }

        public static void ConfigurationBusinessLogicManagers(this IServiceCollection builder, string connectionString)
        {
            builder.ConfigurationDataAccessServices(connectionString);
            ConfigurationManagers(builder);
        }
    }
}

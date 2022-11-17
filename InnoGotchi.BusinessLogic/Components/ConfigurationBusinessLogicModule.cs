using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.BusinessLogic.MappingProfiles;
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
            service.AddScoped<IBodyPartService, BodyPartService>();
            service.AddScoped<ICollaboratorService, CollaboratorService>();
            service.AddScoped<IFeedService, FeedService>();
        }

        private static void ConfigurationMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<PetMapProfile>();
                cfg.AddProfile<FarmMapProfile>();
                cfg.AddProfile<UserMapProfile>();
                cfg.AddProfile<RoleMapProfile>();
                cfg.AddProfile<BodyPartMapProfile>();
                cfg.AddProfile<BodyPartTypeMapProfile>();
                cfg.AddProfile<CollaboratorMapProfile>();
                cfg.AddProfile<VitalSignMapProfile>();
                cfg.AddProfile<FeedMapProfile>();
            });
        }

        public static void ConfigurationBusinessLogicManagers(this IServiceCollection builder, string connectionString)
        {
            builder.ConfigurationDataAccessServices(connectionString);
            builder.AddDataValidation();
            ConfigurationManagers(builder);
            ConfigurationMapProfiles(builder);
        }

        public static void Configure(this IApplicationBuilder app)
        {
            app.ConfigureDataAccessLayer();
        }
    }
}

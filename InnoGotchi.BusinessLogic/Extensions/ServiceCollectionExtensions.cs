using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.BusinessLogic.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using InnoGotchi.DataAccess.Extensions;
using System.Reflection;
using InnoGotchi.Components.Extensions;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder, string connectionString)
        {
            ConfigureServices(builder);
            ConfigureMapProfiles(builder);
            ConfigureDtoValidators(builder);

            builder.ConfigureDataAccessLayer(connectionString);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IPetService, PetService>();
            service.AddScoped<IFarmService, FarmService>();
            service.AddScoped<IIdentityService, IdentityService>();
            service.AddScoped<IVitalSignService, VitalSignService>();
            service.AddScoped<IBodyPartService, BodyPartService>();
            service.AddScoped<ICollaboratorService, CollaboratorService>();
            service.AddScoped<IFeedService, FeedService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
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

        private static void ConfigureDtoValidators(IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("InnoGotchi.Components"));
            services.AddValidatorsFromAssembly(assembly);
        }
    }
}

using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.DataAccess.Components
{
    public static class ConfigurationDataAccessModule
    {
        public static void ConfigurationDataAccessServices(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<InnoGotchiDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(IRepository<>), typeof(InnoGotchiRepository<>));

            builder.AddScoped<DbContext, InnoGotchiDbContext>();
        }

        public static void ConfigureDataAccessLayer(this IApplicationBuilder app)
        {
            app.Migrate<InnoGotchiDbContext>();
        }
    }
}

using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<InnoGotchiDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(IRepository<>), typeof(InnoGotchiRepository<>));

            builder.AddScoped<DbContext, InnoGotchiDbContext>();
        }
    }
}

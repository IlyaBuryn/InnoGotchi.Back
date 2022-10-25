using FluentValidation;
using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using InnoGotchi.DataAccess.Repositories;
using InnoGotchi.DataAccess.Validators;
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

            builder.AddScoped<IRepository<Pet>, InnoGotchiRepository<Pet>>();
            builder.AddScoped<IRepository<Farm>, InnoGotchiRepository<Farm>>();
            builder.AddScoped<IRepository<Collaborator>, InnoGotchiRepository<Collaborator>>();
            builder.AddScoped<IRepository<VitalSign>, InnoGotchiRepository<VitalSign>>();
            builder.AddScoped<IRepository<BodyPart>, InnoGotchiRepository<BodyPart>>();
            builder.AddScoped<IRepository<BodyPartType>, InnoGotchiRepository<BodyPartType>>();
            builder.AddScoped<IRepository<IdentityUser>, InnoGotchiRepository<IdentityUser>>();
            builder.AddScoped<IRepository<IdentityRole>, InnoGotchiRepository<IdentityRole>>();

            builder.AddScoped<IValidator<IdentityUser>, IdentityUserValidator>();

            builder.AddScoped<DbContext, InnoGotchiDbContext>();
        }
    }
}

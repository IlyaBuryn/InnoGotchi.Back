using FluentValidation;
using FluentValidation.AspNetCore;
using InnoGotchi.DataAccess.Models;
using InnoGotchi.DataAccess.Models.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.DataAccess.Components
{
    public static class DtoValidatorExtension
    {
        public static void AddDataValidation(this IServiceCollection services)
        {
            services.AddFluentValidation();

            services.AddTransient<IValidator<IdentityRole>, IdentityRoleValidator>();
            services.AddTransient<IValidator<IdentityUser>, IdentityUserValidator>();
            services.AddTransient<IValidator<Collaborator>, CollaboratorValidator>();
            services.AddTransient<IValidator<Farm>, FarmValidator>();
            services.AddTransient<IValidator<Pet>, PetValidator>();
            services.AddTransient<IValidator<VitalSign>, VitalSignValidator>();
            services.AddTransient<IValidator<BodyPart>, BodyPartValidator>();
            services.AddTransient<IValidator<Feed>, FeedValidator>();
        }
    }
}

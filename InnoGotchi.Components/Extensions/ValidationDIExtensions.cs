using FluentValidation;
using InnoGotchi.Components.DtoValidation.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InnoGotchi.Components.Extensions
{
    public static class ValidationDIExtensions
    {
        public static void AddValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetCustomAttribute<ValidatorAttribute>() != null);
            foreach (var type in types)
            {
                var dtoType = type.GetCustomAttribute<ValidatorAttribute>().DtoType;
                var validatorType = typeof(IValidator<>).MakeGenericType(dtoType);
                services.AddTransient(validatorType, type);
            }
        }
    }
}

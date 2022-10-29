﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.DataAccess.Components
{
    public static class MigrationExtension
    {
        public static void Migrate<TContext>(this IApplicationBuilder builder) where TContext : DbContext
        {
            using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();

            var sp = ctx.GetInfrastructure();

            ctx.Database.Migrate();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WSA.Microservice.AuthSample.Application.Common.Interfaces.Repositories;
using WSA.Microservice.AuthSample.Infrastructure.Persistence.Contexts;
using WSA.Microservice.AuthSample.Infrastructure.Persistence.Repositories;

namespace WSA.Microservice.AuthSample.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database context
            services.AddDbContext<TemplateContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(TemplateContext).Assembly.FullName)));

            // Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ITodoRepository, TodoRepository>();
        }
    }
}

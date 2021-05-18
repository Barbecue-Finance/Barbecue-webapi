using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Services.AutoMapperProfiles;
using Services.Common.Abstractions;
using Services.Common.Implementations;

namespace Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBarbecueDependencies(this IServiceCollection services)
        {
            // DbContext will take connection string from Environment or throw
            services.AddDbContext<BarbecueDbContext>();

            services.AddAkianaRepositories();

            services.AddBarbecueApiServices();
            
            services.AddBarbecueMobileServices();

            services.AddBarbecueCommonServices();
            
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BarbecueAutomapperProfile>();
            });

            services.AddScoped<Func<Type, object, Task<bool>>>(x =>
            {
                async Task<bool> Func(Type t, object o)
                {
                    var dbContext = x.GetRequiredService<BarbecueDbContext>();
                    return await dbContext.FindAsync(t, o) != null;
                }

                return Func;
            });

            return services;
        }

        public static IServiceCollection AddAkianaRepositories(this IServiceCollection services)
        {
            // Add Repositories
            return services;
        }

        public static IServiceCollection AddBarbecueCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IRequestCounterService>(new RequestCounterService());
            return services;
        }
        
        public static IServiceCollection AddBarbecueApiServices(this IServiceCollection services)
        {
            // Add Services
            return services;
        }

        public static IServiceCollection AddBarbecueMobileServices(this IServiceCollection services)
        {
            // Add Services
            return services;
        }
    }
}
using System;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Services.ApiServices.Abstractions;
using Services.ApiServices.Implementations;
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

            services.AddBarbecueRepositories();

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

        public static IServiceCollection AddBarbecueRepositories(this IServiceCollection services)
        {
            // Add Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenSessionRepository, TokenSessionRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();
            services.AddScoped<IPurseRepository, PurseRepository>();
            services.AddScoped<IIncomeMoneyOperationRepository, IncomeMoneyOperationRepository>();
            services.AddScoped<IOutComeMoneyOperationRepository, OutComeMoneyOperationRepository>();
            
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

            services.AddScoped<ITokenSessionService, TokenSessionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInviteService, InviteService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPurseService, PurseService>();
            services.AddScoped<IMoneyOperationService, MoneyOperationService>();
            
            return services;
        }

        public static IServiceCollection AddBarbecueMobileServices(this IServiceCollection services)
        {
            // Add Services
            return services;
        }
    }
}
using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Models.DTOs.Users;
using Services.ApiServices.Abstractions;

namespace Seeder
{
    public class SeedData
    {
        private IServiceScope _serviceScope;

        private IUserService _userService;

        public SeedData(IServiceProvider provider)
        {
            _serviceScope = provider.CreateScope();
            Context = _serviceScope.ServiceProvider.GetRequiredService<BarbecueDbContext>();

            _userService = _serviceScope.ServiceProvider.GetRequiredService<IUserService>();
        }

        ~SeedData()
        {
            _serviceScope.Dispose();
        }

        private BarbecueDbContext Context { get; set; }

        public async Task Seed()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.EnsureCreatedAsync();

            Console.WriteLine("Database dropped and recreated");

            await _userService.Create(new CreateUserDto()
            {
                Login = "User1",
                Password = "User1",
                Username = "Unique Username1"
            });
            
            await _userService.Create(new CreateUserDto()
            {
                Login = "User2",
                Password = "User2",
                Username = "Unique Username2"
            });
        }
    }
}
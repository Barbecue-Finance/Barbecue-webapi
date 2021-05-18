using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Seeder
{
    public class SeedData
    {
        private IServiceScope _serviceScope;

        public SeedData(IServiceProvider provider)
        {
            _serviceScope = provider.CreateScope();
            Context = _serviceScope.ServiceProvider.GetRequiredService<BarbecueDbContext>();
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
        }
    }
}
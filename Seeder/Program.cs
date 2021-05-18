using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Seeder
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddBarbecueDependencies().BuildServiceProvider();

            await new SeedData(serviceProvider).Seed();
            Console.WriteLine("Seeded successfully");
        }
    }
}
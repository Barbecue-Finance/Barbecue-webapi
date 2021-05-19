using System;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Db.Account;
using Models.Db.MoneyOperations;
using Models.Db.Sessions;

namespace Infrastructure
{
    public class BarbecueDbContext : DbContext
    {
        public BarbecueDbContext()
        {
        }

        public BarbecueDbContext(DbContextOptions<BarbecueDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
#if DEBUG
            Console.WriteLine("Using debug connection");
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BarbecueFinance;Username=postgres;Password=root");
#else
            // Console.WriteLine("Using Environment variable connection");
            var connectionString = Environment.GetEnvironmentVariable("CONN_STR");

            if (connectionString == null) throw new ArgumentNullException("env:CONN_STR NOT PASSED");
            optionsBuilder.UseNpgsql(connectionString);
#endif
            // TODO: Remove this line for prod
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserGroups)
                .WithMany(g => g.Users)
                .UsingEntity<UserToUserGroup>(
                    arg => arg.HasOne(e => e.UserGroup).WithMany(g => g.UsersRelation),
                    arg => arg.HasOne(e => e.User).WithMany(u => u.UserGroupsRelation),
                    obj => obj.HasKey(e => new {e.UserId, e.UserGroupId})
                );
        }

        public DbSet<User> UserAccounts { get; set; }

        public DbSet<TokenSession> TokenSessions { get; set; }
        public DbSet<IncomeMoneyOperation> IncomeMoneyOperations { get; set; }
        public DbSet<OutComeMoneyOperation> OutComeMoneyOperations { get; set; }
        public DbSet<Purse> Purses { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
    }
}
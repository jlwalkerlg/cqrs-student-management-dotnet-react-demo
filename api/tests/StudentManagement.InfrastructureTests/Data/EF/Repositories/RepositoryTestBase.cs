using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentManagement.Infrastructure.Data.EF;

namespace StudentManagement.InfrastructureTests.Data.EF.Repositories
{
    public abstract class RepositoryTestBase : IDisposable
    {
        private static readonly DbContextOptions<AppDbContext> options;

        static RepositoryTestBase()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var connectionString = config["DbConnectionString"];

            options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString)
                .Options;
        }

        private static bool migrated = false;
        protected readonly AppDbContext context;

        protected RepositoryTestBase()
        {
            context = new AppDbContext(options);

            if (!migrated)
            {
                context.Database.EnsureCreated();
                migrated = true;
            }

            context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            context.Database.RollbackTransaction();
            context.Dispose();
        }
    }
}

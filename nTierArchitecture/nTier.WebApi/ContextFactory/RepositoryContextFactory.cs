using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using nTier.Repositories.EFCore.Repositories;

namespace nTier.WebApi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
    {
        public RepositoryDbContext CreateDbContext(string[] args)
        {
            //configurationBuilder
            var configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //DbContextOptionBuilder
            var builder = new DbContextOptionsBuilder<RepositoryDbContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                prj => prj.MigrationsAssembly("nTier.WebApi"));

            return new RepositoryDbContext(builder.Options);


        }
    }
}

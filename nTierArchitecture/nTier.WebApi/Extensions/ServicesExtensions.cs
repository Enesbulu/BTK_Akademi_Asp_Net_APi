using Microsoft.EntityFrameworkCore;
using nTier.Repositories.Contracts;
using nTier.Repositories.EFCore;
using nTier.Repositories.EFCore.Repositories;

namespace nTier.WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryDbContext>( //Sql bağlantısında hangi repository kullanılacak belirtilir.
                opt => opt.UseSqlServer(
                    configuration.GetConnectionString("sqlConnection")    //Connection stringlerden hangisi kullanılacak belirtiliyor.
                )
            );

        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}

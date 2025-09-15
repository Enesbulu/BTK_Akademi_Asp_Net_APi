using Microsoft.EntityFrameworkCore;
using WebApi.Modals;
using WebApi.Repositories.Config;

namespace WebApi.Repositories
{
    public class RepositoryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}

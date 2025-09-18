using Microsoft.EntityFrameworkCore;
using nTier.Entites.Models;
using nTier.Repositories.EFCore.Config;

namespace nTier.Repositories.EFCore.Repositories
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}

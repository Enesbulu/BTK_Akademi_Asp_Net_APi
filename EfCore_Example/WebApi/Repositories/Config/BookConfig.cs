using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Modals;

namespace WebApi.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Birinci Kitap", Price = 74 },
                new Book { Id = 2, Title = "BirMasalKitap", Price = 78 },
                new Book { Id = 3, Title = "SEnBEnsin Kitap", Price = 56 },
                new Book { Id = 4, Title = "KEndin Ol Kitap", Price = 10 }
                );
        }
    }
}

using nTier.Entites.Models;
using nTier.Repositories.Contracts;
using nTier.Repositories.EFCore.Repositories;

namespace nTier.Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryDbContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public IQueryable<Book> GetAllBooks(bool trackChanges) => FindAll(trackChanges).OrderBy(b=>b.Id);

        public Book GetOneBookById(int id, bool trackChanges) => FindByCondition(b => b.Id.Equals(id), trackChanges).FirstOrDefault();

        public void UpdateOneBook(Book book) => Update(book);
    }
}

using nTier.Repositories.Contracts;
using nTier.Services.Contracts;

namespace nTier.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager));

        }
        public IBookService BookService => _bookService.Value;
    }
}

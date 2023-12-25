using WebApplication1.Interface;

namespace WebApplication1.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private IAuthorRepository _authors;
        private IBookRepository _books;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
        }

        public IAuthorRepository Authors => _authors ??= new AuthorRepository(_context);

        public IBookRepository Books => _books ??= new BookRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task ClearAllDataAsync()
        {
            _context.Authors.RemoveRange(_context.Authors);
            _context.Books.RemoveRange(_context.Books);

            await SaveChangesAsync();
    
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Interface;
using WebApplication1;
using WebApplication1.Models;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryContext _context;

    public AuthorRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetAuthorById(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthor(Author author)
    {
        _context.Entry(author).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}

namespace WebApplication1.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }

        Task ClearAllDataAsync();
        Task<int> SaveChangesAsync();
    }
}

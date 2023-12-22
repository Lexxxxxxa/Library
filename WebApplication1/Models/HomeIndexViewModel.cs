using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}

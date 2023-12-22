using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _unitOfWork.Authors.GetAllAuthors();
            var books = await _unitOfWork.Books.GetAllBooks();

            var model = (authors, books);

            return View(model);
        }

        public async Task<IActionResult> AuthorDetails(int id)
        {
            var author = await _unitOfWork.Authors.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public async Task<IActionResult> BookDetails(int id)
        {
            var book = await _unitOfWork.Books.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> AddBook(string title, string genre, int authorId)
        {
            var author = await _unitOfWork.Authors.GetAuthorById(authorId);

            if (author != null)
            {
                var newBook = new Book { Title = title, Genre = genre, Author = author };
                await _unitOfWork.Books.AddBook(newBook);
                await _unitOfWork.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name)
        {
            var newAuthor = new Author { Name = name };
            _unitOfWork.Authors.AddAuthor(newAuthor);
            _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearDatabase()
        {
            await _unitOfWork.ClearAllDataAsync();

            return RedirectToAction("Index");
        }
    }
}

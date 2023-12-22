using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.Include(b => b.Author).ToList();
            var authors = _context.Authors.ToList();

            var model = new LibraryViewModel
            {
                Books = books,
                Authors = authors
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddBook(string title, string genre, int authorId)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            if (author != null)
            {
                var newBook = new Book { Title = title, Genre = genre, Author = author };
                _context.Books.Add(newBook);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name)
        {
            var newAuthor = new Author { Name = name };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearDatabase()
        {
            try
            {
                await ClearLibrary(_context);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        private async Task ClearLibrary(LibraryContext context)
        {
            foreach (var book in context.Books)
            {
                context.Books.Remove(book);
            }

            foreach (var author in context.Authors)
            {
                context.Authors.Remove(author);
            }

            await context.SaveChangesAsync();
        }
    }
}

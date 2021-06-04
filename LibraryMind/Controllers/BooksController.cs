using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMind.Data;
using LibraryMind.Models;

namespace LibraryMind.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BookNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "book_name_desc" : "";
            ViewData["AuthorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
            ViewData["PagesSortParm"] = String.IsNullOrEmpty(sortOrder) ? "pages_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var books = from s in _context.Book
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.BookName.Contains(searchString)
                                       || b.Author.Contains(searchString)
                                       || b.Genre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "book_name_desc":
                    books = books.OrderByDescending(b => b.BookName);
                    break;
                case "author_desc":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "genre_desc":
                    books = books.OrderBy(b => b.Genre);
                    break;
                case "pages_desc":
                    books = books.OrderByDescending(b => b.Pages);
                    break;
                case "Date":
                    books = books.OrderBy(s => s.YearPublishing);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.YearPublishing);
                    break;
                default:
                    books = books.OrderBy(b => b.BookName);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Books>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Book
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,BookName,Author,Genre,Pages,YearPublishing")] Books books)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(books);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Не удалось сохранить изменения. " +
                    "Попробуйте ещё раз, и если проблема не исчезнет, " +
                    "обратитесь к системному администратору.");
            }
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Book.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,BookName,Author,Genre,Pages,YearPublishing")] Books books)
        //{
        //    if (id != books.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(books);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BooksExists(books.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(books);
        //}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Book.FirstOrDefaultAsync(b => b.ID == id);
            if (await TryUpdateModelAsync<Books>(
                bookToUpdate,
                "",
                b => b.BookName, b => b.Author, b => b.Genre, b => b.Pages, b => b.YearPublishing))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не удалось сохранить изменения. " +
                        "Попробуйте ещё раз, и если проблема не исчезнет, " +
                        "обратитесь к системному администратору.");
                }
            }
            return View(bookToUpdate);
        }



        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Book
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (books == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Удаление не удалось. Попробуйте ещё раз, и если проблема не исчезнет, " +
                    "обратитесь к системному администратору.";
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Book.FindAsync(id);
            if (books == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Book.Remove(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool BooksExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}

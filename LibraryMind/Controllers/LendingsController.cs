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
    public class LendingsController : Controller
    {
        private readonly LibraryContext _context;

        public LendingsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Lendings
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LendingDateSortParm"] = sortOrder == "Date1" ? "lending_date_desc" : "Date1";
            ViewData["ReturnDateSortParm"] = sortOrder == "Date2" ? "return_desc" : "Date2";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var lendings = from l in _context.Lending.Include(i => i.Book).Include(i => i.Reader)
                           select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                lendings = lendings.Where(l => l.Reader.FIO.Contains(searchString)
                                       || l.Book.BookName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date1":
                    lendings = lendings.OrderByDescending(l => l.LendingDate);
                    break;
                case "lending_date_desc":
                    lendings = lendings.OrderBy(l => l.LendingDate);
                    break;
                case "Date2":
                    lendings = lendings.OrderByDescending(l => l.ReturnDate);
                    break;
                case "return_date_desc":
                    lendings = lendings.OrderBy(l => l.ReturnDate);
                    break;
                default:
                    lendings = lendings.OrderByDescending(s => s.LendingDate);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Lendings>.CreateAsync(lendings.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Lendings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendings = await _context.Lending
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lendings == null)
            {
                return NotFound();
            }

            return View(lendings);
        }

        // GET: Lendings/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "BookName");
            ViewData["ReaderID"] = new SelectList(_context.Reader, "ID", "FIO");
            return View();
        }

        // POST: Lendings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,ReaderID,BookID,LendingDate,ReturnDate")] Lendings lendings)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(lendings);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Не удалось сохранить изменения " +
                    "Попробуйте ещё раз, и если проблема не исчезнет, " +
                    "обратитесь к системному администратору.");
            }
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "ID", lendings.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Reader, "ID", "ID", lendings.ReaderID);
            return View(lendings);
        }

        // GET: Lendings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendings = await _context.Lending.FindAsync(id);
            if (lendings == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "BookName", lendings.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Reader, "ID", "FIO", lendings.ReaderID);
            return View(lendings);
        }

        // POST: Lendings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,ReaderID,BookID,LendingDate,ReturnDate")] Lendings lendings)
        //{
        //    if (id != lendings.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(lendings);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LendingsExists(lendings.ID))
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
        //    ViewData["BookID"] = new SelectList(_context.Book, "ID", "ID", lendings.BookID);
        //    ViewData["ReaderID"] = new SelectList(_context.Reader, "ID", "ID", lendings.ReaderID);
        //    return View(lendings);
        //}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lendingToUpdate = await _context.Lending.FirstOrDefaultAsync(l => l.ID == id);
            if (await TryUpdateModelAsync<Lendings>(
                lendingToUpdate,
                "",
                l => l.ReaderID, l => l.BookID, l => l.LendingDate, l => l.ReturnDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не удалось сохранить изменения " +
                    "Попробуйте ещё раз, и если проблема не исчезнет, " +
                    "обратитесь к системному администратору.");
                }
            }
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "ID", lendingToUpdate.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Reader, "ID", "ID", lendingToUpdate.ReaderID);
            return View(lendingToUpdate);
        }


        // GET: Lendings/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendings = await _context.Lending
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lendings == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Удаление не удалось. Попробуйте ещё раз, и если проблема не исчезнет, " +
                    "обратитесь к системному администратору.";
            }

            return View(lendings);
        }

        // POST: Lendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lendings = await _context.Lending.FindAsync(id);
            if (lendings == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Lending.Remove(lendings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool LendingsExists(int id)
        {
            return _context.Lending.Any(e => e.ID == id);
        }
    }
}

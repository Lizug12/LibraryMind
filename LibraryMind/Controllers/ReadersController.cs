using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMind.Data;
using LibraryMind.Models;
using LibraryMind;

namespace LibraryMind.Controllers
{
    public class ReadersController : Controller
    {
        private readonly LibraryContext _context;

        public ReadersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["FIOSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fio_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var readers = from r in _context.Reader
                           select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                readers = readers.Where(r => r.FIO.Contains(searchString)
                                       || r.Address.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "fio_desc":
                    readers = readers.OrderByDescending(r => r.FIO);
                    break;
                default:
                    readers = readers.OrderBy(r => r.FIO);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Readers>.CreateAsync(readers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readers = await _context.Reader
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (readers == null)
            {
                return NotFound();
            }

            return View(readers);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,FIO,Address,Phone,Age")] Readers readers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(readers);
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
            return View(readers);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readers = await _context.Reader.FindAsync(id);
            if (readers == null)
            {
                return NotFound();
            }
            return View(readers);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,FIO,Address,Phone,Age")] Readers readers)
        //{
        //    if (id != readers.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(readers);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReadersExists(readers.ID))
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
        //    return View(readers);
        //}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var readerToUpdate = await _context.Reader.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Readers>(
                readerToUpdate,
                "",
                r => r.FIO, r => r.Address, r => r.Phone, r => r.Age))
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
            return View(readerToUpdate);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readers = await _context.Reader
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (readers == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Удаление не удалось. Попробуйте ещё раз, и если проблема не исчезнет, " +
                    "обратитесь к системному администратору.";
            }

            return View(readers);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var readers = await _context.Reader.FindAsync(id);
            if (readers == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Reader.Remove(readers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ReadersExists(int id)
        {
            return _context.Reader.Any(e => e.ID == id);
        }
    }
}

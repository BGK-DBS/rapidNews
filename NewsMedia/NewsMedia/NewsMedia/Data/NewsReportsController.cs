#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; //to add authentication
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace NewsMedia.Data
{
    [Authorize]
    public class NewsReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsReports
        public async Task<IActionResult> Index()
        {
            // return View(await _context.NewsReport.ToListAsync());

            var CurrentUser = User.Identity.Name;

            var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

            return View(newsReport);
        }

        public async Task<IActionResult> ListByUser()
        {
            var CurrentUser = User.Identity.Name;

            var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

            return View(newsReport);
        }

        public async Task<IActionResult> ListByTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                return NotFound();
            }

            var newsReport = _context.NewsReport.Where(m => m.Title == title);

            if (newsReport == null)
            {
                return NotFound();
            }

            return View(newsReport);
        }

        public async Task<IActionResult> ListByCategory(string Category)
        {
            if (String.IsNullOrEmpty(Category))
            {
                return NotFound();
            }

            var newsReport = _context.NewsReport.Where(m => m.Category == Category);

            if (newsReport == null)
            {
                return NotFound();
            }

            return View(newsReport);
        }

        public async Task<IActionResult> EditByUser()
        {
            var CurrentUser = User.Identity.Name;

            var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

            return View(newsReport);
        }

        // GET: NewsReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsReport = await _context.NewsReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsReport == null)
            {
                return NotFound();
            }

            return View(newsReport);
        }

        // GET: NewsReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Category")] NewsReport newsReport)
        {
            if (ModelState.IsValid)
            {
                newsReport.CreationDate = DateTime.Now;
                newsReport.CreationEmail = User.Identity.Name;
                _context.Add(newsReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsReport);
        }

        // GET: NewsReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsReport = await _context.NewsReport.FindAsync(id);
            if (newsReport == null)
            {
                return NotFound();
            }
            return View(newsReport);
        }

        // POST: NewsReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Category")] NewsReport newsReport)
        {
            if (id != newsReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    newsReport.CreationDate = DateTime.Now;
                    newsReport.CreationEmail = User.Identity.Name;
                    _context.Update(newsReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsReportExists(newsReport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsReport);
        }

        // GET: NewsReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsReport = await _context.NewsReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsReport == null)
            {
                return NotFound();
            }

            return View(newsReport);
        }

        // POST: NewsReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsReport = await _context.NewsReport.FindAsync(id);
            _context.NewsReport.Remove(newsReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsReportExists(int id)
        {
            return _context.NewsReport.Any(e => e.Id == id);
        }
    }
}

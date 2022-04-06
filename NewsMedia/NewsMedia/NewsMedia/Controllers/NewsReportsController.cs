#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; //to add authentication
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NewsMedia.Data;
using NewsMedia.Services;

namespace NewsMedia.Controllers
{
    [Authorize]
    public class NewsReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ReportsApiClient _reportsApiClient;

        public NewsReportsController(ApplicationDbContext context, ReportsApiClient reportsApiClient)
        {
            _context = context;
            _reportsApiClient = reportsApiClient;
        }

        // GET: NewsReports
        public async Task<IActionResult> Index()
        {
            // return View(await _context.NewsReport.ToListAsync());

            var CurrentUser = User.Identity.Name;

            // amending to call webapi to get the full list of reports 
            return View(await _reportsApiClient.GetReportList());

            //var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);
            //return View(newsReport);
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
            // Amended to use webapi and remove local db call 

            int ReportId = id.Value;
            var newsReport = await _reportsApiClient.GetReportItem(ReportId);

            //var newsReport = await _context.NewsReport
            //    .FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> Create([Bind("Id,Title,Body,CreationDate,LastModifiedDate,Category,CreationEmail")] NewsReport newsReport)
        {
            newsReport.CreationDate = DateTime.Now;
            newsReport.LastModifiedDate = DateTime.Now;
            newsReport.CreationEmail = User.Identity.Name;
            // removed validation as kept getting an error requires more work. 
            //if (ModelState.IsValid)
            //{
                // Amended to use webapi and remove local db call 
                await _reportsApiClient.CreateReportItem(newsReport);
                
                //_context.Add(newsReport);
                //await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            //}
            //return View(newsReport);
        }

        // GET: NewsReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Amended to use webapi and remove local db call 

            int ReportId = id.Value;
            var newsReport = await _reportsApiClient.GetReportItem(ReportId);
            //var newsReport = await _context.NewsReport.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,CreationDate,LastModifiedDate,Category,CreationEmail")] NewsReport newsReport)
        {
            if (id != newsReport.Id)
            {
                return NotFound();
            }
 
            if (ModelState.IsValid)
            {
                // Amended to use webapi and remove local db call & associated error handling 
                newsReport.LastModifiedDate = DateTime.Now; 
                await _reportsApiClient.UpdateReportItem(id, newsReport);
                //try
                //{
                //    newsReport.CreationDate = DateTime.Now;
                //    newsReport.CreationEmail = User.Identity.Name;
                //    _context.Update(newsReport);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!NewsReportExists(newsReport.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
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
            // Amended to use webapi and remove local db call 

            int ReportId = id.Value;
            var newsReport = await _reportsApiClient.GetReportItem(ReportId);
            //var newsReport = await _context.NewsReport
            //    .FirstOrDefaultAsync(m => m.Id == id);
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
            // Call delete service
            await _reportsApiClient.DeleteReportItem(id);

            //var newsReport = await _context.NewsReport.FindAsync(id);
            //_context.NewsReport.Remove(newsReport);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsReportExists(int id)
        {
            return _context.NewsReport.Any(e => e.Id == id);
        }
    }
}

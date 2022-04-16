#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; //To control user access
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NewsMedia.Data;
using NewsMedia.Services;

namespace NewsMedia.Controllers
{
    [Authorize]
    public class CommentItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CommentsApiClient _commentsApiClient;

        public CommentItemsController(ApplicationDbContext context, CommentsApiClient commentsApiClient)
        {
            _context = context;
            _commentsApiClient = commentsApiClient;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {

            var CurrentUser = User.Identity.Name;

            var reportIDSearch = 0;
            return View(await _commentsApiClient.GetCommentListByFilter(CurrentUser, reportIDSearch));

        }

        public async Task<IActionResult> CommentListByUser()
        {
            var CurrentUser = User.Identity.Name;

            var commentItem = _context.CommentItem.Where(m => m.CreatedBy == CurrentUser);

            return View(commentItem);
        }


        ////public async Task<IActionResult> ListByCategory(string Category)
        ////{
        ////    if (String.IsNullOrEmpty(Category))
        ////    {
        ////        return NotFound();
        ////    }

        ////    var newsReport = _context.NewsReport.Where(m => m.Category == Category);

        ////    if (newsReport == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return View(newsReport);
        ////}

        //public async Task<IActionResult> EditByUser()
        //{
        //    var CurrentUser = User.Identity.Name;

        //    var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

        //    return View(newsReport);
        //}

        //// GET: CommentItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int intID = id.Value;
            var commentItem = await _commentsApiClient.GetCommentItem(intID);

            if (commentItem == null)
            {
                return NotFound();
            }

            return View(commentItem);
            }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,CommentText,ReportID,DateCeated")] CommentItem commentItem)
        {
            if (ModelState.IsValid)
            {
                await _commentsApiClient.CreateCommentItem(commentItem);
                return RedirectToAction(nameof(Index));
            }
            return View(commentItem);
        }


        // GET: CommentItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int intID = id.Value;
            var commentItem = await _commentsApiClient.GetCommentItem(intID);

            if (commentItem == null)
            {
                return NotFound();
            }
            return View(commentItem);
        }

        //// GET: NewsReports/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    // Amended to use webapi and remove local db call 

        //    int ReportId = id.Value;
        //    var newsReport = await _reportsApiClient.GetReportItem(ReportId);
        //    //var newsReport = await _context.NewsReport
        //    //    .FirstOrDefaultAsync(m => m.Id == id);
        //    if (newsReport == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(newsReport);
        //}

        //// POST: NewsReports/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    // Call delete service
        //    await _reportsApiClient.DeleteReportItem(id);

        //    //var newsReport = await _context.NewsReport.FindAsync(id);
        //    //_context.NewsReport.Remove(newsReport);
        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool NewsReportExists(int id)
        //{
        //    return _context.NewsReport.Any(e => e.Id == id);
        //}

        //public List<Category> GetCategories()
        //{
        //    //var Categories = new List<CategoryList>();
        //    //Categories.Add(new CategoryList() { Id = 1, ListOfCategories = "National" });
        //    //Categories.Add(new CategoryList() { Id = 2, ListOfCategories = "International" });
        //    //Categories.Add(new CategoryList() { Id = 3, ListOfCategories = "Entretaiment" });
        //    //Categories.Add(new CategoryList() { Id = 4, ListOfCategories = "Sports" });

        //    var categories = _context.Category.ToList();

        //    return categories;


        //}
    }
}
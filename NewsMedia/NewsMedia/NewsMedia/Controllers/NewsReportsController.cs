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
using NewsMedia.Models;

namespace NewsMedia.Controllers
{
    [Authorize]
    public class NewsReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ReportsApiClient _reportsApiClient;
        private CommentsApiClient _commentsApiClient;

        public NewsReportsController(ApplicationDbContext context, ReportsApiClient reportsApiClient, CommentsApiClient commentsApiClient)
        {
            _context = context;
            _reportsApiClient = reportsApiClient;
            _commentsApiClient = commentsApiClient;
        }

        // GET: NewsReports
        public async Task<IActionResult> Index()
        {
            // return View(await _context.NewsReport.ToListAsync());
            //var CurrentUser = User.Identity.Name;


            var CurrentUser = User.Identity.Name;

            // amending to call webapi to get list of reports filtered by the logged in user 
            //var searchCategory = 0;
            var newsReports = await _reportsApiClient.GetReportList();//brings all news reports
            //var newsReports = await _reportsApiClient.GetReportList();

            // commenting out code that uses local db to get list of logged in user 
            //var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

            var viewModels = new List<NewsReportViewModel>();

            foreach (var nr in newsReports)
            {
                var temp = new NewsReportViewModel();

                temp.Id = nr.Id;
                temp.Title = nr.Title;
                temp.Body = nr.Body;
                temp.CreationDate = nr.CreationDate;
                //var test = (Category)_context.Category.Where(c => c.Id == Convert.ToInt32(nr.Category));

                //temp.CategoryName = ((Category)_context.Category.FirstOrDefault(c => c.Id == nr.CategoryId)).Name;

                var category = ((Category)_context.Category.FirstOrDefault(c => c.Id == nr.CategoryId));
                if (category == null)
                {
                    temp.CategoryName = "Invalid";
                }
                else
                {
                    temp.CategoryName = category.Name;
                }


                temp.CreationEmail = nr.CreationEmail;
                viewModels.Add(temp);

            };

            //return View(await _reportsApiClient.GetReportList());
            //await _reportsApiClient.GetReportList();
            return View(viewModels);


        }



        //return View(await _reportsApiClient.GetReportList());
        //await _reportsApiClient.GetReportList();



        //public async Task<IActionResult> ListByUser()
        //        {
        //            var CurrentUser = User.Identity.Name;

        //            var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

        //            return View(newsReport);
        //        }

        //public async Task<IActionResult> ListByUser()
        //        {
        //            var CurrentUser = User.Identity.Name;

        //            var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

        //            return View(newsReport);
        //        }


        //public async Task<IActionResult> ListByTitle(string title)
        //{
        //    if (String.IsNullOrEmpty(title))
        //    {
        //        return NotFound();
        //    }

        //    var newsReport = _context.NewsReport.Where(m => m.Title == title);

        //    if (newsReport == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(newsReport);
        //}

        //public async Task<IActionResult> ListByCategory(string Category)
        //{
        //    if (String.IsNullOrEmpty(Category))
        //    {
        //        return NotFound();
        //    }

        //    var newsReport = _context.NewsReport.Where(m => m.Category == Category);

        //    if (newsReport == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(newsReport);
        //}

        //public async Task<IActionResult> EditByUser()
        //{
        //    var CurrentUser = User.Identity.Name;

        //    var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);

        //    return View(newsReport);
        //}

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
            var temp = new NewsReportViewModel();

            temp.Id = newsReport.Id;
            temp.Title = newsReport.Title;
            temp.Body = newsReport.Body;
            temp.CreationDate = newsReport.CreationDate;
            //var test = (Category)_context.Category.Where(c => c.Id == Convert.ToInt32(nr.Category));

            //temp.CategoryName = ((Category)_context.Category.FirstOrDefault(c => c.Id == nr.CategoryId)).Name;

            var category = ((Category)_context.Category.FirstOrDefault(c => c.Id == newsReport.CategoryId));
            if (category == null)
            {
                temp.CategoryName = "Invalid";
            }
            else
            {
                temp.CategoryName = category.Name;
            }

            temp.CreationEmail = newsReport.CreationEmail;


            //var newsReport = await _context.NewsReport
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if (newsReport == null)
            {
                return NotFound();
            }

            //BC Adding in  list of comments 
            var reportComments = new ReportComments();

            var comments = await _commentsApiClient.GetCommentListByFilter("", ReportId);

            reportComments.NewsReportItem = temp;
            reportComments.CommentsList = (List<CommentItem>)comments;

            return View(reportComments);

            //return View(temp);
        }

        public enum Publish { yes, no };


        private void SetViewBagPublishType(Publish Publish)
        {
            IEnumerable<Publish> values = Enum.GetValues(typeof(Publish))

                .Cast<Publish>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString(),
                                                    Selected = value == Publish,
                                                };
            ViewBag.PublishType = items;

        }

        public ActionResult items()
        {
            SetViewBagPublishType(Publish.yes);

            return View();
        }

        [HttpPost]
        public ActionResult itemsPost()
        {
            ViewBag.messageString = Publish.yes;
            return View();
        }

        // GET: NewsReports/Create
        public IActionResult Create()

        {
            ViewBag.CategoriesSelectList = new SelectList(GetCategories(), "Id", "Name");
            return View();
        }

        // POST: NewsReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,CategoryId")] NewsReport newsReport)

        {
            newsReport.CreationDate = DateTime.Now;
            newsReport.LastModifiedDate = DateTime.Now;
            newsReport.CreationEmail = User.Identity.Name;

            // Amended to use webapi and remove local db call 

            var tempnewsreport = newsReport;
            await _reportsApiClient.CreateReportItem(newsReport);

            //_context.Add(newsReport);
            //await _context.SaveChangesAsync();
            ViewBag.CategoriesSelectList = new SelectList(GetCategories(), "Id", "Name");
            return RedirectToAction(nameof(Index));

            //ViewBag.CategoriesSelectList = new SelectList(GetCategories(), "Id", "ListOfCategories");
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
            //BC Adding in  list of comments 
            var reportComments = new ReportComments();
            int ReportId = id.Value;
            var newsReport = await _reportsApiClient.GetReportItem(ReportId);
            //var newsReport = await _context.NewsReport.FindAsync(id);
            if (newsReport == null)
            {
                return NotFound();
            }
            ViewBag.CategoriesSelectList = new SelectList(GetCategories(), "Id", "Name");
            var temp = new NewsReportViewModel();

            temp.Id = newsReport.Id;
            temp.Title = newsReport.Title;
            temp.Body = newsReport.Body;
            temp.CreationDate = newsReport.CreationDate;
            //var test = (Category)_context.Category.Where(c => c.Id == Convert.ToInt32(nr.Category));

            //temp.CategoryName = ((Category)_context.Category.FirstOrDefault(c => c.Id == nr.CategoryId)).Name;

            var category = ((Category)_context.Category.FirstOrDefault(c => c.Id == newsReport.CategoryId));
            if (category == null)
            {
                temp.CategoryName = "Invalid";
            }
            else
            {
                temp.CategoryName = category.Name;
            }


            temp.CreationEmail = newsReport.CreationEmail;
            //BC Adding in  list of comments 
            //var reportComments = new ReportComments();

            var comments = await _commentsApiClient.GetCommentListByFilter("", ReportId);

            reportComments.NewsReportItem = temp;

            reportComments.ReportItem = newsReport;
            reportComments.ReportItem = newsReport;

            reportComments.CommentsList = (List<CommentItem>)comments;

            return View(reportComments);

            //return View(newsReport);
        }

        // POST: NewsReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,CreationDate,LastModifiedDate,CategoryId,CreationEmail")] ReportComments reportComments)
        public async Task<IActionResult> Edit(int id, ReportComments reportComments)
        {
            if (id != reportComments.ReportItem.Id)
            {
                return NotFound();
            }

            // Amended to use webapi and remove local db call & associated error handling 

            reportComments.ReportItem.LastModifiedDate = DateTime.Now;
            await _reportsApiClient.UpdateReportItem(id, reportComments.ReportItem);
            return RedirectToAction(nameof(Index));
            reportComments.ReportItem.LastModifiedDate = DateTime.Now;
            await _reportsApiClient.UpdateReportItem(id, reportComments.ReportItem);
            return RedirectToAction(nameof(Index));

        }

        // GET: NewsReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var CurrentUser = User.Identity.Name;



            if (id == null)
            {
                return NotFound();
            }
            // Amended to use webapi and remove local db call 

            int ReportId = id.Value;
            var newsReport = await _reportsApiClient.GetReportItem(ReportId);
            //var newsReport = _context.NewsReport.Where(m => m.CreationEmail == CurrentUser);


            var temp = new NewsReportViewModel();

            temp.Id = newsReport.Id;
            temp.Title = newsReport.Title;
            temp.Body = newsReport.Body;
            temp.CreationDate = newsReport.CreationDate;
            //var test = (Category)_context.Category.Where(c => c.Id == Convert.ToInt32(nr.Category));

            //temp.CategoryName = ((Category)_context.Category.FirstOrDefault(c => c.Id == nr.CategoryId)).Name;

            var category = ((Category)_context.Category.FirstOrDefault(c => c.Id == newsReport.CategoryId));
            if (category == null)
            {
                temp.CategoryName = "Invalid";
            }
            else
            {
                temp.CategoryName = category.Name;
            }


            temp.CreationEmail = newsReport.CreationEmail;



            if (newsReport == null)
            {
                return NotFound();
            }

            //BC Adding in  list of comments 
            var reportComments = new ReportComments();

            var comments = await _commentsApiClient.GetCommentListByFilter("", ReportId);

            reportComments.NewsReportItem = temp;
            reportComments.CommentsList = (List<CommentItem>)comments;

            return View(reportComments);
            //return View(temp);
        }

        // POST: NewsReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Call delete service
            await _reportsApiClient.DeleteReportItem(id);

            // BC - delete all related comments



            var reportComments = new ReportComments();

            var comments = await _commentsApiClient.GetCommentListByFilter("", id);

            reportComments.CommentsList = (List<CommentItem>)comments;

            for (int i = 0; i < reportComments.CommentsList.Count; i++)
            {
                var comment = reportComments.CommentsList[i];
                await _commentsApiClient.DeleteCommentItem(comment.Id);

            }

            //var newsReport = await _context.NewsReport.FindAsync(id);
            //_context.NewsReport.Remove(newsReport);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsReportExists(int id)
        {
            return _context.NewsReport.Any(e => e.Id == id);
        }


        public List<Publish> GetPublish()
        {
            var Publish = new List<Publish>();

            var publish = _context.publish.ToList();

            return publish;

        }
        public List<Category> GetCategories()
        {
            //var Categories = new List<CategoryList>();
            //Categories.Add(new CategoryList() { Id = 1, ListOfCategories = "National" });
            //Categories.Add(new CategoryList() { Id = 2, ListOfCategories = "International" });
            //Categories.Add(new CategoryList() { Id = 3, ListOfCategories = "Entretaiment" });
            //Categories.Add(new CategoryList() { Id = 4, ListOfCategories = "Sports" });

            var categories = _context.Category.ToList();

            return categories;


        }
    }
}

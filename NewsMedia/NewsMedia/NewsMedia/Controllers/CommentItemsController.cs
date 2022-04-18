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
        private int reportIdentifier;

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
        // BC - Required CommentListByUser and listByCategory? 
        //public async Task<IActionResult> CommentListByUser()
        //{
        //    var CurrentUser = User.Identity.Name;

        //    var commentItem = _context.CommentItem.Where(m => m.CreatedBy == CurrentUser);

        //    return View(commentItem);
        //}


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

        //// GET: Comments/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Comments/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CommentText,ReportId")] CommentItem commentItem)
        //{
        //    commentItem.DateCreated = DateTime.Now;
        //    commentItem.CreatedBy = User.Identity.Name;
        //    //if (ModelState.IsValid)
        //    //{
        //    await _commentsApiClient.CreateCommentItem(commentItem);
        //    return RedirectToAction(nameof(Index));
        //    //}
        //    //return View(commentItem);
        //}
        //BC - create comment linked to report start
        // GET: Comments/Create/{ReportId}
        public IActionResult Create()
        { 
            return View();

        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentText,ReportId")] CommentItem commentItem)
        {
            commentItem.DateCreated = DateTime.Now;
            commentItem.CreatedBy = User.Identity.Name;
            commentItem.ReportId = 0;
            if (TempData.ContainsKey("Report Id"))
                { 
                    var commentreportID = TempData["Report Id"].ToString();
                if (!(commentreportID == null))
                    {
                    bool IsParsable = Int32.TryParse(commentreportID, out reportIdentifier);
                    if (IsParsable)
                        {
                        commentItem.ReportId = reportIdentifier;
                        }
                    }
                }
 

            //if (ModelState.IsValid)
            //{
            await _commentsApiClient.CreateCommentItem(commentItem);
            return RedirectToAction("Index", "NewsReports");
            //}
            //return View(commentItem);
        }
        //BC - End create linked to report ID

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

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedBy,CommentText,ReportId,DateCreated")] CommentItem commentItem)
        {
            if (id != commentItem.Id)
            {
                return NotFound();
            }

            // Call Comments WebAPI and remove calls to local db

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(comment);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CommentExists(comment.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            await _commentsApiClient.UpdateCommentItem(id, commentItem);
            return RedirectToAction(nameof(Index));
            //}
            //return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Call Comments WebAPI and remove calls to local db
            //var comment = await _context.Comment
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (comment == null)
            //{
            //    return NotFound();
            //}

            //return View(comment);

            int intID = id.Value;
            var commentItem = await _commentsApiClient.GetCommentItem(intID);

            if (commentItem == null)
            {
                return NotFound();
            }
            return View(commentItem);

        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Call Comments WebAPI and remove calls to local db
            //var comment = await _context.Comment.FindAsync(id);
            //_context.Comment.Remove(comment);
            //await _context.SaveChangesAsync();

            // Call delete service
            await _commentsApiClient.DeleteCommentItem(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.CommentItem.Any(e => e.Id == id);
        }



    }
}
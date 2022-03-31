#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsMedia.Data;

namespace NewsMedia.Controllers
{
    public class CommentItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CommentItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CommentItem.ToListAsync());
        }

        // GET: CommentItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentItem = await _context.CommentItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentItem == null)
            {
                return NotFound();
            }

            return View(commentItem);
        }

        // GET: CommentItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommentItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,CommentText,ReportId,DateCreated")] CommentItem commentItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentItem);
                await _context.SaveChangesAsync();
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

            var commentItem = await _context.CommentItem.FindAsync(id);
            if (commentItem == null)
            {
                return NotFound();
            }
            return View(commentItem);
        }

        // POST: CommentItems/Edit/5
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentItemExists(commentItem.Id))
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
            return View(commentItem);
        }

        // GET: CommentItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentItem = await _context.CommentItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentItem == null)
            {
                return NotFound();
            }

            return View(commentItem);
        }

        // POST: CommentItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentItem = await _context.CommentItem.FindAsync(id);
            _context.CommentItem.Remove(commentItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentItemExists(int id)
        {
            return _context.CommentItem.Any(e => e.Id == id);
        }
    }
}

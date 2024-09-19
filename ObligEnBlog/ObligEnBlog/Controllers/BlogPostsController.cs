using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Controllers {
    public class BlogPostsController : Controller {
        private readonly ObligEnBlogContext _context;

        public BlogPostsController(ObligEnBlogContext context) {
            _context = context;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index() {
            return _context.BlogPost != null ?
                        View(await _context.BlogPost.ToListAsync()) :
                        Problem("Entity set 'ObligEnBlogContext.BlogPost'  is null.");
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id) {

            if (id == null || _context.BlogPost == null) {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null) {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Create
        public IActionResult Create() {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogPostId,BlogParentId,Title,Description,Content")] BlogPost blogPost) {
            if (ModelState.IsValid) {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.BlogPost == null) {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null) {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogPostId,Title,Description,Content")] BlogPost blogPost) {
            if (id != blogPost.BlogPostId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BlogPostExists(blogPost.BlogPostId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.BlogPost == null) {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null) {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.BlogPost == null) {
                return Problem("Entity set 'ObligEnBlogContext.BlogPost'  is null.");
            }
            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost != null) {
                _context.BlogPost.Remove(blogPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id) {
            return (_context.BlogPost?.Any(e => e.BlogPostId == id)).GetValueOrDefault();
        }
    }
}

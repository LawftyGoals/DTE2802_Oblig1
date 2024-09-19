using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;
using ObligEnBlog.Models.ViewModels;

namespace ObligEnBlog {
    public class BlogsController : Controller {
        private readonly ObligEnBlogContext _context;

        public BlogsController(ObligEnBlogContext context) {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index() {
            return _context.Blog != null ?
                        View(await _context.Blog.ToListAsync()) :
                        Problem("Entity set 'ObligEnBlogContext.Blog'  is null.");
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id) {
            Console.WriteLine("#############################################################################");
            Console.WriteLine(id);
            if (id == null || _context.Blog == null) {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.BlogId == id);

            var blogPosts = await _context.BlogPost.Where(m => m.BlogParentId == blog.BlogId).ToListAsync();
            if (blog == null) {
                return NotFound();
            }
            var myView = new BlogDetailsViewModel { Blog = blog, BlogPosts = blogPosts };

            return View(myView);
        }

        // GET: Blogs/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Name,Description,DateCreated")] Blog blog) {
            if (ModelState.IsValid) {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Blog == null) {
                return NotFound();
            }

            var blog = await _context.Blog.FindAsync(id);
            if (blog == null) {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Name,Description,DateCreated")] Blog blog) {
            if (id != blog.BlogId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BlogExists(blog.BlogId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Blog == null) {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null) {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Blog == null) {
                return Problem("Entity set 'ObligEnBlogContext.Blog'  is null.");
            }
            var blog = await _context.Blog.FindAsync(id);
            if (blog != null) {
                _context.Blog.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id) {
            return (_context.Blog?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}

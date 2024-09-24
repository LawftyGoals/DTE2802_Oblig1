using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;
using ObligEnBlog.Models.ViewModels;
using System.Text.Encodings.Web;

namespace ObligEnBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ObligEnBlogContext _context;

        public BlogPostsController(ObligEnBlogContext context)
        {
            _context = context;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index()
        {
            return _context.BlogPost != null ?
                        View(await _context.BlogPost.ToListAsync()) :
                        Problem("Entity set 'ObligEnBlogContext.BlogPost'  is null.");
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.BlogPost == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.BlogPostId == id);

            if (blogPost == null)
            {
                return NotFound();
            }


            var comments = await _context.Comment.Where(m => m.BlogPostParentId == id).ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }

            var myView = new BlogPostDetailsViewModel { BlogPost = blogPost, Comments = comments };

            return View(myView);
        }

        // GET: BlogPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            var parentBlog = await _context.Blog.FirstAsync(m => m.BlogId == blogPost.BlogParentId);

            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Blogs", new { id = parentBlog.BlogId });
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlogPost == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogPostId,Title,Description,Content")] BlogPost blogPost)
        {
            if (id != blogPost.BlogPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.BlogPostId))
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
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BlogPost == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlogPost == null)
            {
                return Problem("Entity set 'ObligEnBlogContext.BlogPost'  is null.");
            }
            var blogPost = await _context.BlogPost.FindAsync(id);


            if (blogPost != null)
            {
                var comments = await _context.Comment.Where((c) => c.BlogPostParentId == blogPost.BlogPostId).ToListAsync();

                _context.Comment.RemoveRange(comments);
            }


            if (blogPost != null)
            {
                _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), "Blogs", new { id = blogPost.BlogParentId});
            }
            return RedirectToAction(nameof(Index), "Blogs");
        }

        private bool BlogPostExists(int id)
        {
            return (_context.BlogPost?.Any(e => e.BlogPostId == id)).GetValueOrDefault();
        }
    }
}

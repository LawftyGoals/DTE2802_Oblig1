using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Models.Entities;
using ObligEnBlog.Models.Repository;
using ObligEnBlog.Models.ViewModels;

namespace ObligEnBlog {
    public class BlogsController : Controller {
        private IBlogRepository blogRepository;

        public BlogsController(IBlogRepository repository) {
            blogRepository = repository;
        }

        // GET: Blogs
        public async Task<IActionResult> Index() {
            var blogs = blogRepository.GetAllBlogs().ToList();
            return View(blogs);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id) {

            if (id == null || blogRepository.GetAllBlogs() == null) {
                return NotFound();
            }

            var blog = blogRepository.GetBlogById(id);

            var blogPosts = blogRepository.GetAllBlogPosts().Where(m => m.BlogParentId == blog.BlogId).ToList();
            if (blog == null) {
                return NotFound();
            }

            var user = blogRepository.GetUser(blog.OwnerId);

            var myView = new BlogDetailsViewModel { Blog = blog, BlogPosts = blogPosts, User = user };

            return View(myView);
        }

        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create() {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Name,Description,DateCreated,OwnerId,Owner")] Blog blog) {
            if (ModelState.IsValid) {
                blogRepository.AddBlog(blog);
                blogRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || blogRepository.GetAllBlogs() == null) {
                return NotFound();
            }

            var blog = blogRepository.GetBlogById(id);
            if (blog == null) {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Name,Description,DateCreated,Active,OwnerId,Owner")] Blog blog) {
            if (id != blog.BlogId) {
                return NotFound();
            }
            Console.WriteLine(ModelState.IsValid);
            Console.WriteLine(blog.ToString());

            if (ModelState.IsValid) {
                try {
                    blogRepository.UpdateBlog(blog);
                    blogRepository.Save();
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || blogRepository.GetAllBlogs() == null) {
                return NotFound();
            }

            var blog = blogRepository.GetBlogById(id);
            if (blog == null) {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (blogRepository.GetAllBlogs() == null) {
                return Problem("Entity set 'ObligEnBlogContext.Blog'  is null.");
            }
            var blog = blogRepository.GetBlogById(id);

            if (blog != null) {
                var blogPosts = blogRepository.GetAllBlogPosts().Where((bp) => bp.BlogParentId == blog.BlogId).ToList();
                var blogPostIds = blogPosts.Select(bp => bp.BlogPostId).ToList();
                var comments = blogRepository.GetAllComments().Where(c => blogPostIds.Contains(c.BlogPostParentId)).ToList();

                if (comments != null) {
                    blogRepository.DeleteComments(comments);
                }

                if (blogPosts != null) {
                    blogRepository.DeleteBlogPosts(blogPosts);
                }

                if (blog != null) {
                    blogRepository.DeleteBlog(id);
                }
            }

            blogRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id) {
            var blog = blogRepository.GetBlogById(id);

            if (blog != null) { return true; }

            return false;
        }
    }
}

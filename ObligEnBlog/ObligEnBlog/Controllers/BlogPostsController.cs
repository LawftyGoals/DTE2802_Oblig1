using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Models.Entities;
using ObligEnBlog.Models.Repository;
using ObligEnBlog.Models.ViewModels;

namespace ObligEnBlog.Controllers {
    public class BlogPostsController : Controller {
        private IBlogRepository _repository;

        public BlogPostsController(IBlogRepository blogRepository) {
            _repository = blogRepository;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index() {
            var blogPosts = _repository.GetAllBlogPosts();
            return blogPosts != null ?
                        View(blogPosts.ToList()) :
                        Problem("Entity set 'ObligEnBlogContext.BlogPost'  is null.");
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id) {

            if (id == null || _repository.GetAllBlogPosts() == null) {
                return NotFound();
            }

            var blogPost = _repository.GetBlogPostById(id);

            if (blogPost == null) {
                return NotFound();
            }


            var comments = _repository.GetAllComments().Where(m => m.BlogPostParentId == id).ToList();
            var blog = GetParentBlog(blogPost.BlogParentId);

            if (comments == null) {
                return NotFound();
            }

            var myView = new BlogPostDetailsViewModel { BlogPost = blogPost, Comments = comments, Blog = blog };

            return View(myView);
        }

        // GET: BlogPosts/Create
        [Authorize]
        public IActionResult Create() {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost) {
            var parentBlog = GetParentBlog(blogPost.BlogParentId);

            if (ModelState.IsValid && parentBlog != null) {
                _repository.AddBlogPost(blogPost);
                _repository.Save();
                return RedirectToAction(nameof(Details), "Blogs", new { id = parentBlog.BlogId });
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _repository.GetAllBlogPosts() == null) {
                return NotFound();
            }

            var blogPost = _repository.GetBlogPostById(id);
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
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("BlogPostId,Title,Description,Content,OwnerId,Owner")] BlogPost blogPost) {
            if (id != blogPost.BlogPostId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _repository.UpdateBlogPost(blogPost);
                    _repository.Save();
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _repository.GetAllBlogPosts() == null) {
                return NotFound();
            }

            var blogPost = _repository.GetBlogPostById(id);
            if (blogPost == null) {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_repository.GetAllBlogPosts() == null) {
                return Problem("Entity set 'ObligEnBlogContext.BlogPost'  is null.");
            }
            var blogPost = _repository.GetBlogPostById(id);


            if (blogPost != null) {
                var comments = _repository.GetAllComments().Where((c) => c.BlogPostParentId == blogPost.BlogPostId).ToList();

                _repository.DeleteComments(comments);
            }


            if (blogPost != null) {
                _repository.DeleteBlogPost(blogPost.BlogPostId);
                _repository.Save();

                return RedirectToAction(nameof(Details), "Blogs", new { id = blogPost.BlogParentId });
            }
            return RedirectToAction(nameof(Index), "Blogs");
        }

        private bool BlogPostExists(int id) {
            if (_repository.GetBlogPostById(id) != null) return true;
            return false;
        }

        private Blog? GetParentBlog(int id) {
            var blog = _repository.GetBlogById(id);

            return blog;
        }


    }


}

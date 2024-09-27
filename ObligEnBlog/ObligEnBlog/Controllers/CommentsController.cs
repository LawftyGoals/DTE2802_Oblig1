using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Models.Entities;
using ObligEnBlog.Models.Repository;
using ObligEnBlog.Models.ViewModels;

namespace ObligEnBlog.Controllers {
    public class CommentsController : Controller {
        private IBlogRepository _repository;

        public CommentsController(IBlogRepository blogRepository) {
            _repository = blogRepository;
        }

        // GET: Comments
        public async Task<IActionResult> Index() {
            var comments = _repository.GetAllComments();

            return comments != null ?
                        View(comments.ToList()) :
                        Problem("Entity set 'ObligEnBlogContext.Comment'  is null.");
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _repository.GetAllComments() == null) {
                return NotFound();
            }

            var comment = _repository.GetCommentById(id);
            if (comment == null) {
                return NotFound();
            }

            var blogPostParent = GetParentBlogPost(comment.BlogPostParentId);

            var blogParent = GetParentBlog(blogPostParent.BlogParentId);

            var myView = new BlogPostDetailsViewModel() { Blog = blogParent, BlogPost = blogPostParent, Comment = comment };

            return View(myView);
        }

        // GET: Comments/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,BlogPostParentId,CommentText")] Comment comment) {
            var parentBlogPost = GetParentBlogPost(comment.BlogPostParentId);
            if (ModelState.IsValid) {
                _repository.AddComment(comment);
                _repository.Save();
                return RedirectToAction(nameof(Details), "BlogPosts", new { id = parentBlogPost.BlogPostId });
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _repository.GetAllComments() == null) {
                return NotFound();
            }

            var comment = _repository.GetCommentById(id);
            if (comment == null) {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,BlogPostParentId,CommentText")] Comment comment) {
            if (id != comment.CommentId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _repository.UpdateComment(comment);
                    _repository.Save();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!CommentExists(comment.CommentId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _repository.GetAllComments() == null) {
                return NotFound();
            }

            var comment = _repository.GetCommentById(id);
            if (comment == null) {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_repository.GetAllComments() == null) {
                return Problem("Entity set 'ObligEnBlogContext.Comment'  is null.");
            }
            var comment = _repository.GetCommentById(id);
            if (comment != null) {
                _repository.DeleteComment(comment.CommentId);
            }

            _repository.Save();

            if (comment != null) { return RedirectToAction(nameof(Details), "BlogPosts", new { id = comment.BlogPostParentId }); }

            return RedirectToAction(nameof(Details), "Blogs");



        }

        private bool CommentExists(int id) {
            var comment = _repository.GetCommentById(id);

            return comment != null ? true : false;
        }

        private Blog? GetParentBlog(int id) {
            var blog = _repository.GetBlogById(id);

            return blog;
        }

        private BlogPost? GetParentBlogPost(int id) {
            var blogPost = _repository.GetBlogPostById(id);

            return blogPost;
        }
    }
}

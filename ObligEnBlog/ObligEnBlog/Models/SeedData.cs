using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Data;
using ObligEnBlog.Models.Entities;

namespace ObligEnBlog.Models {
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {

            using (var context = new ObligEnBlogContext(serviceProvider.GetRequiredService<DbContextOptions<ObligEnBlogContext>>())) {


                if (false) {
                    RemoveAll(context);
                }

                var hasher = new PasswordHasher<IdentityUser>();
                var defaultUser = new IdentityUser {
                    Id = "default",
                    UserName = "default@example.com",
                    NormalizedUserName = "DEFAULT@EXAMPLE.COM",
                    Email = "default@example.com",
                    NormalizedEmail = "DEFAULT@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "TestTest1."),
                    SecurityStamp = String.Empty
                };

                var secondUser = new IdentityUser {
                    Id = "second",
                    UserName = "second@example.com",
                    NormalizedUserName = "SECOND@EXAMPLE.COM",
                    Email = "second@example.com",
                    NormalizedEmail = "SECOND@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "TestTest2."),
                    SecurityStamp = String.Empty
                };

                var users = new IdentityUser[] { defaultUser, secondUser };

                if (!context.User.Any()) {


                    users = AddUsers(users, context);
                    Console.WriteLine("Users Seeded");

                }

                if (context.Blog.Any() && context.BlogPost.Any() && context.Comment.Any()) {
                    return;
                }



                var blogList = new Blog[] { new Blog { Name = "Best blog", Description = "the best blog created. period.", OwnerId = users.ElementAt(0).Id, Owner = users.ElementAt(0) }, new Blog { Name = "awesome blog", Description = "for all things awesome.", OwnerId = users.ElementAt(1).Id, Owner = users.ElementAt(1) }, new Blog { Name = "just a blog", Description = "we deal in mediocraty", OwnerId = users.ElementAt(0).Id, Owner = users.ElementAt(0) } };


                if (!context.Blog.Any()) {
                    blogList = AddBlogs(blogList, context);
                    Console.WriteLine("seeded Blog");
                }


                var blogPostList = new BlogPost[] { new BlogPost { BlogParentId = blogList.ElementAt(0).BlogId, Title = "What does best mean?", Content = "Simply the best", Description = "a short blog about what the best means", OwnerId = users.ElementAt(0).Id, Owner = users.ElementAt(0) }, new BlogPost { BlogParentId = blogList.ElementAt(1).BlogId, Title = "Awesome. Yes?", Content = "Are questions awesome or not? We the awesome blog say yes.", Description = "a short blog about what the best means", OwnerId = users.ElementAt(1).Id, Owner = users.ElementAt(1) } };

                if (!context.BlogPost.Any()) {
                    blogPostList = AddBlogPosts(blogPostList, context);
                    Console.WriteLine("seeded BlogPost");
                }

                var commentList = new Comment[] { new Comment { BlogPostParentId = blogPostList.ElementAt(0).BlogPostId, CommentText = "Hey bro, real informative, thanks.", OwnerId = users.ElementAt(0).Id, Owner = users.ElementAt(0) }, new Comment { BlogPostParentId = blogPostList.ElementAt(1).BlogPostId, CommentText = "this was a mid-blogpost, do better.", OwnerId = users.ElementAt(0).Id, Owner = users.ElementAt(0) } };

                if (!context.Comment.Any()) {
                    AddComments(commentList, context);
                    Console.WriteLine("seeded Comment");
                }



            }
        }

        private static IdentityUser[] AddUsers(IdentityUser[] users, ObligEnBlogContext context) {
            context.User.AddRange(users);
            context.SaveChanges();

            return context.User.ToArray();
        }

        private static Blog[] AddBlogs(Blog[] blogs, ObligEnBlogContext context) {
            context.Blog.AddRange(blogs);
            context.SaveChanges();

            return context.Blog.ToArray();
        }

        private static BlogPost[] AddBlogPosts(BlogPost[] blogPosts, ObligEnBlogContext context) {
            context.BlogPost.AddRange(blogPosts);
            context.SaveChanges();

            return context.BlogPost.ToArray();
        }
        private static Comment[] AddComments(Comment[] comments, ObligEnBlogContext context) {
            context.Comment.AddRange(comments);
            context.SaveChanges();

            return context.Comment.ToArray();
        }

        private static void RemoveAll(ObligEnBlogContext context) {
            context.Comment.RemoveRange(context.Comment.ToArray());
            context.BlogPost.RemoveRange(context.BlogPost.ToArray());
            context.Blog.RemoveRange(context.Blog.ToArray());
            context.User.RemoveRange(context.User.ToArray());
            context.SaveChanges();
        }

    }

}

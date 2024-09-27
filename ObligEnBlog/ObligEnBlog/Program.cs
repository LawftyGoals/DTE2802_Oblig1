using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObligEnBlog.Authorization;
using ObligEnBlog.Data;
using ObligEnBlog.Models;
using ObligEnBlog.Models.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthorizationHandler,
   BlogIsOwnerAuthorizationHandler>();
builder.Services.AddTransient<IBlogRepository, BlogRepository>();
builder.Services.AddDbContext<ObligEnBlogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ObligEnBlogContext")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ObligEnBlogContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var servcies = scope.ServiceProvider;
    SeedData.Initialize(servcies);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blogs}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

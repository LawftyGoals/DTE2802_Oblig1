﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObligEnBlog.Data;
using ObligEnBlog.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ObligEnBlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ObligEnBlogContext") ?? throw new InvalidOperationException("Connection string 'ObligEnBlogContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ObligEnBlogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ObligEnBlogContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servcies = scope.ServiceProvider;
    SeedData.Initialize(servcies);
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blogs}/{action=Index}/{id?}");

app.Run();

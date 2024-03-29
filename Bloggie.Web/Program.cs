using Bloggie.Web.Data;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* Start Adding Services after creation of WebApp Builder object*/
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add BloggieDb Service
builder.Services.AddDbContext<BloggieDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString"))
    );
//Adding BloggieAuthDb service
builder.Services.AddDbContext<AuthDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieAuthDbConnectionString"))
    );
//Registering Identity
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddScoped<ITagRepository,TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
/* Ending services */


//Creating Web App using Builder object
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

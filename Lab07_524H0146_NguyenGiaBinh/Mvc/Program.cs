using Microsoft.EntityFrameworkCore;
using School;
var b=WebApplication.CreateBuilder(args);
b.Services.AddDbContext<SchoolDb>(o=>o.UseSqlServer(b.Configuration.GetConnectionString("School")));
b.Services.AddControllersWithViews();
var app=b.Build();
app.MapGet("/",()=>Results.Redirect("/Departments"));
app.MapControllerRoute("default","{controller}/{action=Index}/{id?}");
app.Run();

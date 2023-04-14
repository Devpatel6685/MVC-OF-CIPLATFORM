using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.repository.Repository;
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using MVC_OF_CI_PLATFORM.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CIPLATFORMDbContext>();
builder.Services.AddScoped<IUserInterface,UserRepository>();
builder.Services.AddScoped<IMissionInterface ,MissionRepository>();
builder.Services.AddScoped<ISubheaderInterface,SubHeaderRepository>();
builder.Services.AddScoped<IMissionInterface,MissionRepository>();
builder.Services.AddScoped<IVolunterstoryInterface,VolunteerstoryListingRepository>();
builder.Services.AddScoped<IVolunteerInterface,VolunteerRepository>();
builder.Services.AddScoped<IAdminInterface, AdminRepository>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddCloudscribePagination();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(option =>
   {
       option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
       option.LoginPath = "/Home/LOGIN";
       option.AccessDeniedPath = "/Home/LOGIN";
   }
    );
builder.Services.AddSession(Options =>
{  Options.IdleTimeout = TimeSpan.FromMinutes(15);
    Options.Cookie.HttpOnly= true;
    Options.Cookie.IsEssential= true;   
    });
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Mission}/{action=platformLanding}/{id?}");

app.Run();

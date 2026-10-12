using BLL;
using BLL.Service;
using DAL.EF;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;  // authentication authorization
using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// DI for DbContext
builder.Services.AddDbContext<TutorConnectContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

// DI For AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Scope
builder.Services.AddScoped<CourseRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<DepartmentRepo>();
builder.Services.AddScoped<StudentRequestRepo>();
builder.Services.AddScoped<TutorOfferingRepo>();

builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<StudentRequestService>();
builder.Services.AddScoped<TutorOfferingService>();

// MVC 
builder.Services.AddScoped<LoginService>();

// Session Config.
builder.Services.AddDistributedMemoryCache(); // Stores session data in RAM
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10080); // Session expiration 
    options.Cookie.HttpOnly = true; // Protects the session cookie from client-side scripts
    options.Cookie.IsEssential = true; // Crucial for GDPR/cookie consent compliance
});


// Authenticatino Cookie Config.
builder.Services
    .AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";

        options.Cookie.Name = "TutorConnect.UserAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;

        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = false;

        options.Events.OnRedirectToLogin = context =>
        {
            var redirectUri = QueryHelpers.AddQueryString(
                context.RedirectUri,
                "message",
                "login-required");

            context.Response.Redirect(redirectUri);
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization();



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

app.UseRouting();

// Enable Session middleware
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

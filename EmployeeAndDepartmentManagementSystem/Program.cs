using EmployeeAndDepartmentManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("ConStr");

// Register DAL classes with the connection string
builder.Services.AddScoped<EmployeeRepository>(provider => new EmployeeRepository(connectionString));
builder.Services.AddScoped<DepartmentRepository>(provider => new DepartmentRepository(connectionString));
builder.Services.AddScoped<LoginRepository>(provider => new LoginRepository(connectionString));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Add this line to enable session
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();
    if (!path.StartsWith("/login") && string.IsNullOrEmpty(context.Session.GetString("User")))
    {
        context.Response.Redirect("/Login/Login");
    }
    else
    {
        await next();
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

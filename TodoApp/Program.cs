using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services.TodoService;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TodoAppContext>((services, options) =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<ITodoService, TodoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Todo}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
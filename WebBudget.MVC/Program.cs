using WebBudget.Infrastructure.Persistance;
using WebBudget.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using WebBudget.Infrastructure.Seeders;
using WebBudget.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

//do aplikacji Scope przypisuję CreateScope, aby później go użyć z mojego 
// Extension'a 
var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<WebBudgetSeeder>();


//seeduje moje pierwsze dane;
await seeder.Seed();


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

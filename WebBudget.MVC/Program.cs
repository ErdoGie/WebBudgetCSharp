using WebBudget.Infrastructure.Persistance;
using WebBudget.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using WebBudget.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

//do aplikacji Scope przypisujê CreateScope, aby póŸniej go u¿yæ z mojego 
// Extension'a 
var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<WebBudgetSeeder>();


//seeduje moje pierwsze dane;
await seeder.Seed();


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
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

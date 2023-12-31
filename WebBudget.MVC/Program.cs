using WebBudget.Infrastructure.Persistance;
using WebBudget.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using WebBudget.Infrastructure.Seeders;
using WebBudget.Application.Extensions;
using Microsoft.AspNetCore.Identity;
using WebBudget.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

//do aplikacji Scope przypisuję CreateScope, aby później go użyć z mojego 
// Extension'a 
var scope = app.Services.CreateScope();



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
	pattern: "{controller=Home}/{action=Home}/{id?}");

app.MapRazorPages();
app.Run();
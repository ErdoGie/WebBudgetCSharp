using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;
using WebBudget.Infrastructure.Persistance;
using WebBudget.Infrastructure.Repositories;
using WebBudget.Infrastructure.Seeders;


namespace WebBudget.Infrastructure.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			//rejestruje klase UserContext, od teraz mogę wstrzykiwać UserContext dowolnie w projekcie
			services.AddScoped<IUserContext, UserContext>();

			services.AddDbContext<WebBudgetDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebBudget")));

			services.AddScoped<WebBudgetSeeder>();

			// metoda rozszerzająca w warstwie Apliaction, rejestrując nowy serwis.
			services.AddScoped <IWebBudgetRepository, WebBudgetRepository>();

			services.AddDefaultIdentity<IdentityUser>()
				.AddEntityFrameworkStores<WebBudgetDbContext>();

		}
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Infrastructure.Persistance;

namespace WebBudget.Infrastructure.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddDbContext<WebBudgetDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebBudget")));
		}

	}
}

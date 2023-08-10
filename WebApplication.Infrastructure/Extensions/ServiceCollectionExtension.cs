﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			services.AddDbContext<WebBudgetDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebBudget")));

			services.AddScoped<WebBudgetSeeder>();

			// metoda rozszerzająca w warstwie Apliaction, rejestrując nowy serwis.
			services.AddScoped <IWebBudgetRepository, WebBudgetRepository>();

		}
	}
}

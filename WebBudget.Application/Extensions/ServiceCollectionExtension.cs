using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.Mappings;
using WebBudget.Application.Services;
using WebBudget.Application.WebBudget;

namespace WebBudget.Application.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddApplication(this IServiceCollection services)
		{
			// teraz mogę rejestrować zależności
			//przekazuje interfejse, a jego implementacją - typem kontenera zależności jest WebBudgetService

			services.AddScoped<IWebBudgetService, WebBudgetService>();

			services.AddAutoMapper(typeof(WebBudgetIncomeMappingProfile));


			services.AddValidatorsFromAssemblyContaining<WebBudgetIncomeDTOValidator>()
				.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();

		}
	}
}

using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.Mappings;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;

namespace WebBudget.Application.Extensions
{
    public static class ServiceCollectionExtension
	{
		public static void AddApplication(this IServiceCollection services)
		{
			// teraz mogę rejestrować zależności
			//przekazuje interfejse, a jego implementacją - typem kontenera zależności jest WebBudgetService

			services.AddMediatR(typeof(CreateWebBudgetIncomeCommand));
			services.AddMediatR(typeof(CreateWebBudgetExpenseCommand));

			services.AddAutoMapper(typeof(WebBudgetIncomeMappingProfile));

			services.AddAutoMapper(typeof(WebBudgetExpenseMappingProfile));

			services.AddValidatorsFromAssemblyContaining<CreateWebBudgetIncomeCommandValidator>()
				.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();

			services.AddValidatorsFromAssemblyContaining<CreateWebBudgetExpenseCommandValidator>()
				.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();
		}
	}
}

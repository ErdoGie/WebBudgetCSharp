using AutoMapper;
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
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.CreateExpenseCategory;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Domain.Entities;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddApplication(this IServiceCollection services)
		{
			// teraz mogę rejestrować zależności
			//przekazuje interfejse, a jego implementacją - typem kontenera zależności jest WebBudgetService
			services.AddScoped<IUserContext, UserContext>();
			services.AddMediatR(typeof(CreateWebBudgetIncomeCommand));
			services.AddMediatR(typeof(CreateWebBudgetExpenseCommand));
			services.AddMediatR(typeof(CreateIncomeCategoryCommand));
			services.AddMediatR(typeof(CreateExpenseCategoryCommand));


			// zamieniam z addAutoMapper na AddScoped poniewaz autoMapper wymaga bezparametrowych kostruktorow, a ja niestety juz mam parameter IUSerContext w konstruktorze
			services.AddScoped(provider => new MapperConfiguration(cfg =>
			{
				var scope = provider.CreateScope();
				var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
				cfg.AddProfile(new WebBudgetExpenseMappingProfile(userContext));
				cfg.AddProfile(new WebBudgetIncomeMappingProfile(userContext));
				cfg.AddProfile(new IncomeCategoryMappingProfile(userContext));
				cfg.AddProfile(new ExpenseCategoryMappingProfile(userContext));


				//zeby utworzyc mappera muszę na konfiguracji wywolac metode CreateMapper.
			}).CreateMapper()
			);

		/*	services.AddValidatorsFromAssemblyContaining<CreateWebBudgetIncomeCommandValidator>()
				.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();

			services.AddValidatorsFromAssemblyContaining<CreateWebBudgetExpenseCommandValidator>()
				.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();*/

			services.AddScoped<CalcualteBalance>();


		}
	}
}

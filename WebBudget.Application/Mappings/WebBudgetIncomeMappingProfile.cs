using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetIncome;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.Mappings
{
	public class WebBudgetIncomeMappingProfile : Profile
	{
		public WebBudgetIncomeMappingProfile(IUserContext userContext)
		{
			var user = userContext.GetCurrentlyLoggedUser();

			CreateMap<WebBudgetIncomeDTO, Domain.Entities.WebBudgetIncome>()
				.ForMember(i => i.IncomeType, opt => opt.MapFrom(src => src.IncomeType));

			// muszę zmapować z encji bazodanowej na encje dto

			CreateMap<Domain.Entities.WebBudgetIncome, WebBudgetIncomeDTO>()
				.ForMember(i => i.HasUserAccess, opt => opt.MapFrom(src => user != null && src.CreatedById == user.Id))
				.ForMember(i => i.IncomeType, opt => opt.MapFrom(src => src.IncomeType));


			//tworze metode ktora na podstawie WebBudgetIncomeDTO utworzy mape do typu edycji.
			CreateMap<WebBudgetIncomeDTO, EditWebBudgetIncomeCommand>();

			CreateMap<WebBudgetIncomeDTO, DeleteWebBudgetIncomeeCommand>();

		}
	}
}

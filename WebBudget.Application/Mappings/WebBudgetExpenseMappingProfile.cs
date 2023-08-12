using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget;

namespace WebBudget.Application.Mappings
{
	public class WebBudgetExpenseMappingProfile : Profile
	{
		public WebBudgetExpenseMappingProfile()
		{
			CreateMap<WebBudgetExpenseDTO, Domain.Entities.WebBudgetExpense>()
				.ForMember(i => i.ExpenseType, opt => opt.MapFrom(src => src.ExpenseType));
		}
	}
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.Mappings
{
    public class WebBudgetExpenseMappingProfile : Profile
    {
        // muszę dodać kontekst użytkownika, inaczej nie będę mógł korzystać przy autoryzacji
        public WebBudgetExpenseMappingProfile(IUserContext userContext)
        {

            var user = userContext.GetCurrentlyLoggedUser();

            CreateMap<WebBudgetExpenseDTO, Domain.Entities.WebBudgetExpense>()
                .ForMember(i => i.ExpenseType, opt => opt.MapFrom(src => src.ExpenseType));

            CreateMap<Domain.Entities.WebBudgetExpense, WebBudgetExpenseDTO>()
                .ForMember(e => e.HasUserAccess, opt => opt.MapFrom(src => user != null && src.CreatedById == user.Id))
             .ForMember(i => i.ExpenseType, opt => opt.MapFrom(src => src.ExpenseType));

            CreateMap<WebBudgetExpenseDTO, EditWebBudgetExpenseCommand>();

            CreateMap<WebBudgetExpenseDTO, DeleteWebBudgetExpenseCommand>();

        }
    }
}

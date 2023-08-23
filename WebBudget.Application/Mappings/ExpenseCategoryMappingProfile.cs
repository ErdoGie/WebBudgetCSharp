using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget.Commands.CreateExpenseCategory;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.Mappings
{
    public class ExpenseCategoryMappingProfile :Profile
    {

        public ExpenseCategoryMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentlyLoggedUser();

            CreateMap<CreateExpenseCategoryCommand, ExpenseCategory>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));
        }


    }
}

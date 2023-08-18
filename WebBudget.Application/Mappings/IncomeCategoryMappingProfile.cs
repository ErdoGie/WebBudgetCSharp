using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.Mappings
{
    public class IncomeCategoryMappingProfile : Profile
    {

        public IncomeCategoryMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentlyLoggedUser();

            CreateMap<CreateIncomeCategoryCommand, IncomeCategory>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));
        }

    }
}

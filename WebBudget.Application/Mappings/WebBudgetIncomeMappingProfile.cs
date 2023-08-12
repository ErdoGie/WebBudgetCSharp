using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.Mappings
{
	public class WebBudgetIncomeMappingProfile :Profile
	{
        public WebBudgetIncomeMappingProfile()
        {
            CreateMap<WebBudgetIncomeDTO, Domain.Entities.WebBudgetIncome>()
                .ForMember(i => i.IncomeType, opt => opt.MapFrom(src => src.IncomeType));

            // muszę zmapować z encji bazodanowej na encje dto

            CreateMap<Domain.Entities.WebBudgetIncome, WebBudgetIncomeDTO>()
                .ForMember(i => i.IncomeType, opt => opt.MapFrom(src => src.IncomeType));

        }
    }
}

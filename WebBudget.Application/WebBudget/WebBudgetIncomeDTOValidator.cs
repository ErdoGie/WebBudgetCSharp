using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget
{
	public class WebBudgetIncomeDTOValidator : AbstractValidator<WebBudgetIncomeDTO>
	{
	
		public WebBudgetIncomeDTOValidator()
		{
			RuleFor(i => i.IncomeType)
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(15);

			RuleFor(i => i.IncomeValue)
				.NotEmpty()
				.GreaterThan(0).WithMessage("Value cannot be below zero.");

			RuleFor(i => i.IncomeDate)
				.NotEmpty();
				
		}


	}
}

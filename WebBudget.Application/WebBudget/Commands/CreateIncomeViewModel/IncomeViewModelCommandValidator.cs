using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel
{
	public class IncomeViewModelCommandValidator : AbstractValidator<IncomeViewModelCommand>
	{
		public IncomeViewModelCommandValidator()
		{
			RuleFor(i => i.IncomeCommand.IncomeType)
				.NotEmpty().WithMessage("Category cannot be empty");

			RuleFor(i => i.IncomeCommand.IncomeValue)
				.NotEmpty().WithMessage("Value must be provided")
				.GreaterThan(0).WithMessage("Value must be greater than zero");

			RuleFor(i => i.IncomeCommand.IncomeDate)
				.NotEmpty().WithMessage("Category cannot be empty");

		}
	}

}

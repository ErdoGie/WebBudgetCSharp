using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets
{
	public class EditWebBudgetIncomeCommandValidator : AbstractValidator<EditWebBudgetIncomeCommand>
	{

        public EditWebBudgetIncomeCommandValidator()
        {
			RuleFor(i => i.IncomeType)
				.NotEmpty().WithMessage("Cannot be empty")
				.MinimumLength(3).WithMessage("Minimum text length is 3 characters")
				.MaximumLength(15).WithMessage("Maximum text length is 15 characters");

			RuleFor(i => i.IncomeValue)
				.NotEmpty().WithMessage("Value cannot be empty")
				.GreaterThan(0).WithMessage("Value cannot be below zero.");

			RuleFor(i => i.IncomeDate)
				.NotEmpty().WithMessage("Provide Date.");
		}

    }
}

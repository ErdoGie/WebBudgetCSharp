using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetIncome;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense
{

	public class EditWebBudgetExpenseCommandValidator : AbstractValidator<EditWebBudgetExpenseCommand>
	{

		public EditWebBudgetExpenseCommandValidator()
		{
			RuleFor(i => i.ExpenseType)
				.NotEmpty().WithMessage("Cannot be empty")
				.MinimumLength(3).WithMessage("Minimum text length is 3 characters")
				.MaximumLength(15).WithMessage("Maximum text length is 15 characters");

			RuleFor(i => i.ExpenseValue)
				.NotEmpty().WithMessage("Value cannot be empty")
				.GreaterThan(0).WithMessage("Value cannot be below zero.");

			RuleFor(i => i.ExpenseDate)
				.NotEmpty().WithMessage("Provide Date.");
		}

	}

}

﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget
{
	public class WebBudgetExpenseDTOValidator : AbstractValidator<WebBudgetExpenseDTO>
	{
		public WebBudgetExpenseDTOValidator()
		{

			RuleFor(i => i.ExpenseType)
				.NotEmpty().WithMessage("Provide category")
				.MinimumLength(3).WithMessage("Minimum text length is 3 characters")
				.MaximumLength(15).WithMessage("Maximum text length is 15 characters");


			RuleFor(i => i.ExpenseDate)
				.NotEmpty().WithMessage("Provide Date.");

		}
	}
}
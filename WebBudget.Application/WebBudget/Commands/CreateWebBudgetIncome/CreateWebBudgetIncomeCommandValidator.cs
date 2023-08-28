using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome
{
    public class CreateWebBudgetIncomeCommandValidator : AbstractValidator<CreateWebBudgetIncomeCommand>
    {
        public CreateWebBudgetIncomeCommandValidator()
        {
            RuleFor(i => i.IncomeType)
                .NotEmpty().WithMessage("Cannot be empty");


            RuleFor(i => i.IncomeValue)
                .NotEmpty().WithMessage("Value cannot be empty")
                .GreaterThan(0).WithMessage("Value cannot be below zero.");

            RuleFor(i => i.IncomeDate)
                .NotEmpty().WithMessage("Provide Date.");
        }
    }
}

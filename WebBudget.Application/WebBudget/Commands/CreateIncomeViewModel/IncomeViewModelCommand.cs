using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel
{
    public class IncomeViewModelCommand : IRequest
    {
        public CreateWebBudgetIncomeCommand IncomeCommand { get; set; } = new CreateWebBudgetIncomeCommand();
        public List<IncomeCategory> IncomeCategories { get; set; } = new List<IncomeCategory>();


    }
}

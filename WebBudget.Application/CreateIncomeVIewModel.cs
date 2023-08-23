using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Domain.Entities;

namespace WebBudget.Application
{
	public class CreateIncomeVIewModel
	{
		public CreateWebBudgetIncomeCommand IncomeCommand { get; set; } = new CreateWebBudgetIncomeCommand();
		public List<IncomeCategory> IncomeCategories { get; set; } = new List<IncomeCategory>();

	}
}

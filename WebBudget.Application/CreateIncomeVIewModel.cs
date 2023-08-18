using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;

namespace WebBudget.Application
{
	public class CreateIncomeVIewModel
	{
		public CreateWebBudgetIncomeCommand IncomeCommand { get; set; }
		public List<IncomeCategory> IncomeCategories { get; set; }

	}
}

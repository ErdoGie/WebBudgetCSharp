using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateExpenseViewModel;
using WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel;

namespace WebBudget.Application.WebBudget.Commands.CategoryViewModels
{
	public class CreateExpenseView
	{

		public IEnumerable<WebBudgetExpenseDTO> Expenses { get; set; } = new List<WebBudgetExpenseDTO>();
		public ExpenseViewModelCommand ExpenseCommand { get; set; } = new ExpenseViewModelCommand();



	}
}

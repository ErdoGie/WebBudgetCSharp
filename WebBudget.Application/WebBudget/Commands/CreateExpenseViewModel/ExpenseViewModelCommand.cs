using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.WebBudget.Commands.CreateExpenseViewModel
{
	public class ExpenseViewModelCommand :IRequest
	{
		public CreateWebBudgetExpenseCommand ExpenseCommand { get; set; } = new CreateWebBudgetExpenseCommand();
		public List<ExpenseCategory> ExpenseCategories { get; set; } = new List<ExpenseCategory>();
	}
}

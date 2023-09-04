using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateExpenseCategory;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.WebBudget.Commands.CategoryViewModels
{
	public class ExpenseCategoryViewModel
	{
		public IEnumerable<ExpenseCategory> ExpenseCategories { get; set; } = new List<ExpenseCategory>();

		public CreateExpenseCategoryCommand ExpenseCommand { get; set; } = new CreateExpenseCategoryCommand(); 


	}
}

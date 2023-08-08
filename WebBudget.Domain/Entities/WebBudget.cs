using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class WebBudget
	{
		public  int Id { get; set; }
		public WebBudgetIncome BudgetIncome { get; set; } = default!;

		public WebBudgetExpense BudgetExpense { get; set; } = default!;

		public string EncodedName { get; private set; } = default!;

		public void EncodeIncomeName() => EncodedName = BudgetIncome.IncomeType.ToLower().Replace(" ", "-");
		public void EncodeExpenseName() => EncodedName = BudgetExpense.ExpenseType.ToLower().Replace(" ", "-");


	}

}

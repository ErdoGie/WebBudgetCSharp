using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class WebBudgetExpense
	{
		public int ExpenseId { get; set; }
		public string ExpenseType { get; set; } = default!;
		public DateTime ExpenseDate { get; set; }
		public float ExpenseValue { get; set; }
		public string EncodedExpenseName { get; private set; } = default!;
		public void EncodeExpenseName() => EncodedExpenseName = ExpenseType.ToLower().Replace(" ", "-");
	}
}

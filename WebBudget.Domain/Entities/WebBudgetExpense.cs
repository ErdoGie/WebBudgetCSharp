using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class WebBudgetExpense
	{

        public required int Id { get; set; }
        public string ExpenseType { get; set; } = default!;

		public DateTime TodaysDate { get; set; } = DateTime.UtcNow;

		public DateTime ChosenDate { get; set; }

		public decimal ExpenseValue { get; set; }



	}
}

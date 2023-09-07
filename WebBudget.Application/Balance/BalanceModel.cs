using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.Balance
{
	public class BalanceModel
	{
		public IEnumerable<Domain.Entities.WebBudgetIncome>? Incomes { get; set; }

		public IEnumerable<Domain.Entities.WebBudgetExpense>? Expenses { get; set; }

        public float TotalIncome { get; set; }

        public float TotalExpense { get; set; }

        public float Balance { get; set; }


        public List<ChartData> IncomeChartData { get; set; }
        public List<ChartData> ExpenseChartData { get; set; }
    }
}

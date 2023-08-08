using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class WebBudgetIncome
	{

		public string IncomeType { get; set; } = default!;



		public DateTime IncomeDate { get; set; }

		public float IncomeValue { get; set; }



	}
}

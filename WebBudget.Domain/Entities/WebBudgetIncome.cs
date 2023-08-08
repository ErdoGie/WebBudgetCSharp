using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class WebBudgetIncome
	{

        public  required int Id { get; set; }
        public string IncomeType { get; set; } = default!;

		public DateTime TodaysDate { get; set; } = DateTime.UtcNow;

		public DateTime ChosenDate { get; set; } 

		public decimal IncomeValue { get; set; }



    }
}

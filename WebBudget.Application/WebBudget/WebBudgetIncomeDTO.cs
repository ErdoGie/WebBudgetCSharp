using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget
{
	public class WebBudgetIncomeDTO
	{
		public int IncomeId { get; set; }
		public string IncomeType { get; set; } = default!;
		public DateTime IncomeDate { get; set; }
		public float IncomeValue { get; set; }
	
		public bool HasUserAccess { get; set; }
		public int IncomeCategoryId { get; set; }

	}
}

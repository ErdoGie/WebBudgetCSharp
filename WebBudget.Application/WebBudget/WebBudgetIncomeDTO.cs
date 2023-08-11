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
		[Required(ErrorMessage ="Insert Category")]
		[StringLength(20, MinimumLength = 2)]
		public string IncomeType { get; set; } = default!;

		[Required(ErrorMessage = "Insert Date")]
		public DateTime IncomeDate { get; set; }

		public float IncomeValue { get; set; }

		public string? EncodedIncomeName { get; set; }

		public void EncodeIncomeName() => EncodedIncomeName = IncomeType.ToLower().Replace(" ", "-");


	}
}

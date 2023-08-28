using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class WebBudgetIncome
	{
		public int IncomeId { get; set; }
		public string IncomeType { get; set; } = default!;

		[Column(TypeName = "Date")]
		public DateTime IncomeDate { get; set; }
		public float IncomeValue { get; set; }

		public string? CreatedById { get; set; }
		public IdentityUser? CreatedBy { get; set; }
		
		public int ? IncomeCategoryId { get; set; }


	}
}

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
	public class WebBudgetExpense
	{
		public int ExpenseId { get; set; }
		public string ExpenseType { get; set; } = default!;
		[Column(TypeName = "Date")]
		public DateTime ExpenseDate { get; set; }
		public float ExpenseValue { get; set; }
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public string EncodedExpenseName { get;  set; } = default!;
		public void EncodeExpenseName() => EncodedExpenseName = ExpenseType.ToLower().Replace(" ", "-");

		public int? ExpenseCategoryId { get; set; }

	}
}

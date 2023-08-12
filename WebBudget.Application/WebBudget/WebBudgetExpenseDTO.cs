using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget
{
	public class WebBudgetExpenseDTO
	{
		public string ExpenseType { get; set; } = default!;

		public DateTime ExpenseDate { get; set; }

		public float ExpenseValue { get; set; }

		public string? EncodedExpenseName { get; private set; } = default!;

	//	public void EncodeExpenseName() => EncodedExpenseName = ExpenseType.ToLower().Replace(" ", "-");
	}
}

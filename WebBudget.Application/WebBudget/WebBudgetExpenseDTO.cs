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
		public int ExpenseId { get; set; }
		public string ExpenseType { get; set; } = default!;

		public DateTime ExpenseDate { get; set; }

		public float ExpenseValue { get; set; }

		public bool HasUserAccess { get; set; }

		public int ExpenseCategoryId { get; set; }

	}
}

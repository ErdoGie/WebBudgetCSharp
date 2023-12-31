﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Entities
{
	public class ExpenseCategory
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		public string CategoryName { get; set; } = default!;

		public float? Limit { get; set; }

		public string? Date { get; set; }

		public bool IsLimitSet { get; set; }

		public string UserId { get; set; } = default!;
		public IdentityUser User { get; set; } = default!;
	}
}

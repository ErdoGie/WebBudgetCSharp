﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Entities;

namespace WebBudget.Infrastructure.Persistance
{
	public class WebBudgetDbContext : DbContext
	{
        public WebBudgetDbContext(DbContextOptions<WebBudgetDbContext> options ) :base(options)
        {
            
        }
        public DbSet <Domain.Entities.WebBudgetIncome> WebBudgetIncome { get; set; }
		public DbSet<Domain.Entities.WebBudgetExpense> WebBudgetExpense { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//tworze obie encje za jednym zamachem 
			modelBuilder.Entity<Domain.Entities.WebBudgetIncome>(entity =>
			{
				
					entity.ToTable("WebBudgetIncome");
					entity.HasKey(i => i.IncomeId); 
				
			});

			modelBuilder.Entity<WebBudgetExpense>(entity =>
			{
				entity.ToTable("WebBudgetExpense");
				entity.HasKey(e => e.ExpenseId); 
			});
		}
	}
}

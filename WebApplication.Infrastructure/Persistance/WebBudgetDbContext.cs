using Microsoft.EntityFrameworkCore;
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
        public DbSet <Domain.Entities.WebBudget> WebBudgets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//tworze obie encje za jednym zamachem 
			modelBuilder.Entity<Domain.Entities.WebBudget>(entity =>
			{
				entity.OwnsOne(w => w.BudgetIncome, income =>
				{
					income.ToTable("BudgetIncome");
				});

				entity.OwnsOne(w => w.BudgetExpense, expense =>
				{
					expense.ToTable("BudgetExpense");
				});
			});
		}
	}
}

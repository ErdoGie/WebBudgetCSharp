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

		public DbSet <Domain.Entities.WebBudget> WebBudgets { get; set; }


		//konfiguracja do bazy danych, oraz do jakiego providera się będę łączyć.
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// parametrem przekazywanym jest connection string.
			optionsBuilder.UseSqlServer("Server=RdoG;Database=WebBudgetDB;Trusted_Connection=true;trustServerCertificate=true;");

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
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

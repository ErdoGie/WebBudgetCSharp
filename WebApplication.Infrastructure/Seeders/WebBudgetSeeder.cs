using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Entities;
using WebBudget.Infrastructure.Persistance;

namespace WebBudget.Infrastructure.Seeders
{
	public class WebBudgetSeeder
	{
		private readonly WebBudgetDbContext _dbContext;

		//wstrzykuje zależność do dbContextu
		public WebBudgetSeeder(WebBudgetDbContext dbContext)
		{

			_dbContext = dbContext;
		}

		public async Task Seed()
		{
			//sprawdzam czy połączenie do bazy danych jest OK.
			if (await _dbContext.Database.CanConnectAsync())
			{
				if (!_dbContext.WebBudgets.Any())
				{
					var firstSeeder = new Domain.Entities.WebBudget()
					{
						BudgetIncome = new WebBudgetIncome
						{
							IncomeDate = new DateTime(2023, 01, 17),
							IncomeType = "Wyplata",
							IncomeValue = 4000

						},
						BudgetExpense = new WebBudgetExpense
						{
							ExpenseType = "Koszt pralki",
							ExpenseValue = 500,
							ExpenseDate = new DateTime(2023, 01, 18)

						}

					};

					firstSeeder.EncodeExpenseName();
					firstSeeder.EncodeIncomeName();
					_dbContext.WebBudgets.Add(firstSeeder);
					await _dbContext.SaveChangesAsync();

				}

			}


		}


	}
}

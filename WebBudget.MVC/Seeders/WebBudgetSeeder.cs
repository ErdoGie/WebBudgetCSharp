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
				if (!_dbContext.WebBudgetIncome.Any() && !_dbContext.WebBudgetExpense.Any())
				{
					//oba seedery ustawiam od strzała.
					var income = new WebBudgetIncome
					{

						IncomeDate = new DateTime(2023, 07, 17),
						IncomeType = "Wyplata lipiec",
						IncomeValue = 4000


					};

					var expense = new WebBudgetExpense
					{
						ExpenseType = "Moje urodziny",
						ExpenseValue = 500,
						ExpenseDate = new DateTime(2023, 01, 18)
					};

					income.EncodeIncomeName();
					expense.EncodeExpenseName();

					_dbContext.WebBudgetIncome.Add(income);
					_dbContext.WebBudgetExpense.Add(expense);
					await _dbContext.SaveChangesAsync();
				}

			}

		}


	}


}

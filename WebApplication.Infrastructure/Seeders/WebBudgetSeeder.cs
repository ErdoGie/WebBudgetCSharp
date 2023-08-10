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
					//oba seedery ustawiam od strzała.
					var income = new WebBudgetIncome
					{

						IncomeDate = new DateTime(2023, 07, 17),
						IncomeType = "Wyplata lipiec",
						IncomeValue = 4000


					};
					var expense = new WebBudgetExpense
					{
						ExpenseType = "Moje urodzi ny",
						ExpenseValue = 500,
						ExpenseDate = new DateTime(2023, 01, 18)
					};

					var incomeSeeder = new Domain.Entities.WebBudget()
					{
						BudgetIncome = income
					};

					var expenseSeeder = new Domain.Entities.WebBudget()
					{
						BudgetExpense = expense
					};

					incomeSeeder.BudgetIncome.EncodeIncomeName();
					expenseSeeder.BudgetExpense.EncodeExpenseName();

					_dbContext.WebBudgets.Add(incomeSeeder);
					_dbContext.WebBudgets.Add(expenseSeeder);
					await _dbContext.SaveChangesAsync();

				}

			}


		}


	}
}

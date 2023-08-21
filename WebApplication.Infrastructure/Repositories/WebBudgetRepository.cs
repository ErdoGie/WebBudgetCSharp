using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Entities;
using WebBudget.Domain.Interfaces;
using WebBudget.Infrastructure.Persistance;

// ta definicja pozwoli mi na separacje modułów, dzieki czemu nie muszę dodawać referencji 
// z modulu aplication do modulu infrastructure

namespace WebBudget.Infrastructure.Repositories
{
	// muszę zaimplementować interfejs z Domain'a inaczej nie będzie "połączenia" pomiędzy wartswami
	public class WebBudgetRepository : IWebBudgetRepository
	{
		private readonly WebBudgetDbContext _webBudgetDbContext;

		// muszę zapisać mój webBudget w kontekście bazy danych
		// do tego referencja poprzez konstruktor
		public WebBudgetRepository(WebBudgetDbContext webBudgetdbContext)
		{
			_webBudgetDbContext = webBudgetdbContext;

		}



		public async Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome)
		{

			_webBudgetDbContext.Add(webBudgetIncome);
			await _webBudgetDbContext.SaveChangesAsync();

		}
		public async Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense)
		{

			_webBudgetDbContext.Add(webBudgetExpense);
			await _webBudgetDbContext.SaveChangesAsync();

		}

		//odnosze sie do mojego dbCOntextu
		public async Task<IEnumerable<WebBudgetIncome>> GetAllIncomesForLoggedUser(string userId)
		=> await _webBudgetDbContext.WebBudgetIncome.Where(i => i.CreatedById == userId).ToListAsync();


		public async Task<IEnumerable<WebBudgetExpense>> GetAllExpensesForLoggedUser(string userId)
		=> await _webBudgetDbContext.WebBudgetExpense.Where(i => i.CreatedById == userId).ToListAsync();

		public async Task<Domain.Entities.WebBudgetIncome> GetIncomeByEncodedName(string encodedIncomeName)
		=> await _webBudgetDbContext.WebBudgetIncome.FirstAsync(i => i.EncodedIncomeName == encodedIncomeName);


		public async Task<Domain.Entities.WebBudgetExpense> GetExpenseByEncodedName(string encodedExpenseName)
		=> await _webBudgetDbContext.WebBudgetExpense.FirstAsync(e => e.EncodedExpenseName == encodedExpenseName);


		public async Task CommitChanges()
		=> await _webBudgetDbContext.SaveChangesAsync();


		public async Task<WebBudgetExpense> RemoveExpense(string encodedExpenseName)
		{
			var expense = await _webBudgetDbContext.WebBudgetExpense.FirstAsync(e => e.EncodedExpenseName == encodedExpenseName);

			if (expense != null)
			{
				_webBudgetDbContext.WebBudgetExpense.Remove(expense);
				await _webBudgetDbContext.SaveChangesAsync();
			}

			return expense!;
		}

		public async Task<WebBudgetIncome> RemoveIncome(string encodedIncomeName)
		{
			var income = await _webBudgetDbContext.WebBudgetIncome.FirstAsync(i => i.EncodedIncomeName == encodedIncomeName);

			if (income != null)
			{
				_webBudgetDbContext.WebBudgetIncome.Remove(income);
				await _webBudgetDbContext.SaveChangesAsync();
			}

			return income!;
		}

		public async Task<IEnumerable<WebBudgetIncome>> GetAllUserIncomesFromDateRange(string userId, DateTime beginningDate, DateTime endingDate)
			=> await _webBudgetDbContext.WebBudgetIncome
			.Where(i => i.CreatedById == userId && i.IncomeDate >= beginningDate && i.IncomeDate <= endingDate)
			.ToListAsync();

		public async Task<IEnumerable<WebBudgetExpense>> GetAllUserExpensesFromDateRange(string userId, DateTime beginningDate, DateTime endingDate)
			=> await _webBudgetDbContext.WebBudgetExpense
			.Where(e => e.CreatedById == userId && e.ExpenseDate >= beginningDate && e.ExpenseDate <= endingDate)
			.ToListAsync();

		public bool CheckIfIncomeCategoryExists(string categoryName)
		{
			return _webBudgetDbContext.WebBudgetIncome.Any(i => i.IncomeType.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
		}

		public async Task AddIncomeCategory(IncomeCategory category)
		{

			_webBudgetDbContext.IncomeCategories.Add(category);
			await _webBudgetDbContext.SaveChangesAsync();
		}

		public async Task<List<IncomeCategory>> GetAllIncomeCategoriesForUser(string userId)
		{
			return await _webBudgetDbContext.IncomeCategories
				.Where(category => category.UserId == userId)
				.ToListAsync();
		}

		public async Task AddExpenseCategory(ExpenseCategory category)
		{
			_webBudgetDbContext.ExpenseCategories.Add(category);
			await _webBudgetDbContext.SaveChangesAsync();
		}

		public async Task<List<ExpenseCategory>> GetAllExpenseCategoriesForUser(string userId)
			 => await _webBudgetDbContext.ExpenseCategories
			.Where(e => e.UserId == userId)
			.ToListAsync();
	}
}

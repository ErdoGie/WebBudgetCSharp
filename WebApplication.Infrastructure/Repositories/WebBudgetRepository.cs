using Microsoft.AspNetCore.Mvc;
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

        public async Task<WebBudgetIncome> RemoveIncome(int incomeId)
        {
            var income = await _webBudgetDbContext.WebBudgetIncome.FirstAsync(i => i.IncomeId == incomeId);

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

        public async Task<int?> GetIncomeCategoryIdByNameAsync(string categoryName)
        {
            var selectedCategory = await _webBudgetDbContext.IncomeCategories
                .FirstOrDefaultAsync(c => EF.Functions.Collate(c.CategoryName, "SQL_Latin1_General_CP1_CI_AS") == categoryName);

            return selectedCategory?.CategoryId;
        }

        public async Task<int?> GetExpenseCategoryIdByNameAsync(string categoryName)
        {
            var selectedCategory = await _webBudgetDbContext.ExpenseCategories
            .FirstOrDefaultAsync(c => EF.Functions.Collate(c.CategoryName, "SQL_Latin1_General_CP1_CI_AS") == categoryName);

            return selectedCategory?.CategoryId;
        }

        public async Task DeleteIncomeCategoryAndRelatedIncomesAsync(int categoryId)
        {
            var categoryToDelete = await _webBudgetDbContext.IncomeCategories.FindAsync(categoryId);

            var relatedIncomes = _webBudgetDbContext.WebBudgetIncome
                .Where(income => income.IncomeCategoryId == categoryId)
                .ToList();

            _webBudgetDbContext.WebBudgetIncome.RemoveRange(relatedIncomes);


            _webBudgetDbContext.IncomeCategories.Remove(categoryToDelete!);

            await _webBudgetDbContext.SaveChangesAsync();
        }



        public async Task DeleteExpenseCategoryAndRelateExpensesAsync(int categoryId)
        {
            var categoryToDelete = await _webBudgetDbContext.ExpenseCategories.FindAsync(categoryId);

            var relatedExpenses = _webBudgetDbContext.WebBudgetExpense
                .Where(expense => expense.ExpenseCategoryId == categoryId)
                .ToList();

            _webBudgetDbContext.WebBudgetExpense.RemoveRange(relatedExpenses);


            _webBudgetDbContext.ExpenseCategories.Remove(categoryToDelete!);

            await _webBudgetDbContext.SaveChangesAsync();
        }

        public async Task EditIncomeCategoryAsync(int categoryId, string newCategoryName)
        {
            var categoryToUpdate = await _webBudgetDbContext.IncomeCategories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.CategoryName = newCategoryName;
                await _webBudgetDbContext.SaveChangesAsync();
            }

        }

        public async Task UpdateIncomeCategoryInIncomes(int oldCategoryId, string newCategoryName)
        {
            var incomesWithOldCategory = await _webBudgetDbContext.WebBudgetIncome
                .Where(income => income.IncomeCategoryId == oldCategoryId)
                .ToListAsync();

            foreach (var income in incomesWithOldCategory)
            {
                income.IncomeType = newCategoryName;
                _webBudgetDbContext.Entry(income).State = EntityState.Modified;
            }

            await _webBudgetDbContext.SaveChangesAsync();
        }

        public async Task<WebBudgetIncome> GetIncomeByIncomeId(int incomeId)
        => await _webBudgetDbContext.WebBudgetIncome.FirstAsync(i => i.IncomeId == incomeId);

		public async Task EditExpenseCategoryAsync(int categoryId, string newCategoryName)
		{
			var categoryToUpdate = await _webBudgetDbContext.ExpenseCategories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

			if (categoryToUpdate != null)
			{
				categoryToUpdate.CategoryName = newCategoryName;
				await _webBudgetDbContext.SaveChangesAsync();
			}
		}

		public async Task UpdateExpenseCategoryInExpenses(int oldCategoryId, string newCategoryName)
		{
			var expensesWithOldCategory = await _webBudgetDbContext.WebBudgetExpense
			   .Where(expense => expense.ExpenseCategoryId == oldCategoryId)
			   .ToListAsync();

			foreach (var expense in expensesWithOldCategory)
			{
				expense.ExpenseType = newCategoryName;
				_webBudgetDbContext.Entry(expense).State = EntityState.Modified;
			}

			await _webBudgetDbContext.SaveChangesAsync();
		}
	}
}

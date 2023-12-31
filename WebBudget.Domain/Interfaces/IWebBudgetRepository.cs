﻿using WebBudget.Domain.Entities;

namespace WebBudget.Domain.Interfaces
{
    public interface IWebBudgetRepository
    {

        Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome);
        Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense);


        // muszę mieć osobne Taski w interfejsie, aby pobrac z bazy danych pozniej w DBCOntextie
        Task<IEnumerable<Domain.Entities.WebBudgetIncome>> GetAllIncomesForLoggedUser(string userId);

        Task<IEnumerable<Domain.Entities.WebBudgetExpense>> GetAllExpensesForLoggedUser(string userId);

        Task<Domain.Entities.WebBudgetIncome> GetIncomeByIncomeId(int incomeId);
        Task<Domain.Entities.WebBudgetExpense> GetExpenseById(int expenseId);

        Task<Domain.Entities.WebBudgetExpense> RemoveExpense(int expenseId);

        Task<Domain.Entities.WebBudgetIncome> RemoveIncome(int incomeId);

        Task CommitChanges();

       
        Task AddIncomeCategory(IncomeCategory category);

        Task<List<IncomeCategory>> GetAllIncomeCategoriesForUser(string userId);


        Task AddExpenseCategory(ExpenseCategory category);

        Task<List<ExpenseCategory>> GetAllExpenseCategoriesForUser(string userId);

        Task<int?> GetIncomeCategoryIdByNameAsync(string categoryName);
        Task<int?> GetExpenseCategoryIdByNameAsync(string categoryName);

        Task DeleteIncomeCategoryAndRelatedIncomesAsync(int categoryId, string loggedUserId);

		Task DeleteExpenseCategoryAndRelateExpensesAsync(int categoryId);  

        Task EditIncomeCategoryAsync(int categoryId, string newCategoryName);
        Task UpdateIncomeCategoryInIncomes(int oldCategoryId, string newCategoryName);

		Task EditExpenseCategoryAsync(int categoryId, string newCategoryName, float newLimit, string newDate);
		Task UpdateExpenseCategoryInExpenses(int oldCategoryId, string newCategoryName);

		Task<IEnumerable<Domain.Entities.WebBudgetIncome>> GetIncomesData(DateTime startDate, DateTime endDate, string userId);
		Task<IEnumerable<Domain.Entities.WebBudgetExpense>> GetExpensesData(DateTime startDate, DateTime endDate, string userId);

        Task DeleteEverythingConnectedWithUser(string userId);


	}
}
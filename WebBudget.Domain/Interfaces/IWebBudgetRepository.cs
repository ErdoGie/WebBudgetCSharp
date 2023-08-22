using WebBudget.Domain.Entities;

namespace WebBudget.Domain.Interfaces
{
    public interface IWebBudgetRepository
    {

        Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome);
        Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense);


        // muszę mieć osobne Taski w interfejsie, aby pobrac z bazy danych pozniej w DBCOntextie
        Task<IEnumerable<Domain.Entities.WebBudgetIncome>> GetAllIncomesForLoggedUser(string userId);

        Task<IEnumerable<Domain.Entities.WebBudgetExpense>> GetAllExpensesForLoggedUser(string userId);

        Task<Domain.Entities.WebBudgetIncome> GetIncomeByEncodedName(string encodedIncomeName);
        Task<Domain.Entities.WebBudgetExpense> GetExpenseByEncodedName(string encodedExpenseName);

        Task<Domain.Entities.WebBudgetExpense> RemoveExpense(string endodedExpenseName);

        Task<Domain.Entities.WebBudgetIncome> RemoveIncome(string encodedIncomeName);

        Task CommitChanges();

        Task<IEnumerable<WebBudgetIncome>> GetAllUserIncomesFromDateRange(string userId, DateTime beginningDate, DateTime endingDate);
        Task<IEnumerable<WebBudgetExpense>> GetAllUserExpensesFromDateRange(string userId, DateTime beginningDate, DateTime endingDate);

        Task AddIncomeCategory(IncomeCategory category);

		Task<List<IncomeCategory>> GetAllIncomeCategoriesForUser(string userId);


        Task AddExpenseCategory(ExpenseCategory category);

        Task<List<ExpenseCategory>> GetAllExpenseCategoriesForUser(string userId);

        Task<int?> GetIncomeCategoryIdByNameAsync(string categoryName);

	}
}
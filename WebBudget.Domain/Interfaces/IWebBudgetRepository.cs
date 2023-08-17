namespace WebBudget.Domain.Interfaces
{
    public interface IWebBudgetRepository
    {

        Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome);
        Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense);


        // muszę mieć osobne Taski w interfejsie, aby pobrac z bazy danych pozniej w DBCOntextie
        Task<IEnumerable<Domain.Entities.WebBudgetIncome>> GetAllIncomesForLoggedUser(string userId);

        Task<IEnumerable<Domain.Entities.WebBudgetExpense>> GetAllExpenses();

        Task<Domain.Entities.WebBudgetIncome> GetIncomeByEncodedName(string encodedIncomeName);
        Task<Domain.Entities.WebBudgetExpense> GetExpenseByEncodedName(string encodedExpenseName);

        Task<Domain.Entities.WebBudgetExpense> RemoveExpense(string endodedExpenseName);

        Task<Domain.Entities.WebBudgetIncome> RemoveIncome(string encodedIncomeName);

        Task CommitChanges();
	}
}
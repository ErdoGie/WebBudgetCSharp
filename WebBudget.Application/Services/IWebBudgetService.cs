namespace WebBudget.Application.Services
{
    public interface IWebBudgetService
	{
		Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome);

		Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense);
	}
}
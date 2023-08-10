namespace WebBudget.Application.Services
{
	public interface IWebBudgetService
	{
		Task CreateIncome(Domain.Entities.WebBudget webBudget);

		Task CreateExpense(Domain.Entities.WebBudget webBudget);
	}
}
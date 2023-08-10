namespace WebBudget.Application.Services
{
	public interface IWebBudgetService
	{
		Task Create(Domain.Entities.WebBudget webBudget);
	}
}
using WebBudget.Application.WebBudget;

namespace WebBudget.Application.Services
{
    public interface IWebBudgetService
	{
		Task CreateIncome(WebBudgetIncomeDTO webBudgetIncome);

		Task CreateExpense(WebBudgetExpenseDTO webBudgetExpense);
	
		Task <IEnumerable<WebBudgetIncomeDTO>> GetAllIncomes();
	}
}
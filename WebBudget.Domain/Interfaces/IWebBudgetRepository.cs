using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Domain.Interfaces
{
	public interface IWebBudgetRepository
	{

		Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome);
		Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense);

	}
}

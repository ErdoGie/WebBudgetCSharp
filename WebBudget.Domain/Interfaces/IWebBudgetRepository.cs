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


		// muszę mieć osobne Taski w interfejsie, aby pobrac z bazy danych pozniej w DBCOntextie
		Task <IEnumerable<Domain.Entities.WebBudgetIncome>> GetAllIncomes();

		Task <IEnumerable<Domain.Entities.WebBudgetExpense>> GetAllExpenses();

	}
}

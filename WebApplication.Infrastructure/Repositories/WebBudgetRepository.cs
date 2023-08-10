using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public async Task Create(Domain.Entities.WebBudget webBudget)
		{

			_webBudgetDbContext.Add(webBudget);
			await _webBudgetDbContext.SaveChangesAsync();

		}
	}
}

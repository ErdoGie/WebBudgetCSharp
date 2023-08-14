using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Entities;
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

		public async Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome)
		{

			_webBudgetDbContext.Add(webBudgetIncome);
			await _webBudgetDbContext.SaveChangesAsync();

		}
		public async Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense)
		{

			_webBudgetDbContext.Add(webBudgetExpense);
			await _webBudgetDbContext.SaveChangesAsync();

		}

		//odnosze sie do mojego dbCOntextu
		public async Task<IEnumerable<WebBudgetIncome>> GetAllIncomes()
			=> await _webBudgetDbContext.WebBudgetIncome.ToListAsync();

		public async Task<IEnumerable<WebBudgetExpense>> GetAllExpenses()
		=> await _webBudgetDbContext.WebBudgetExpense.ToListAsync();

		public async Task<Domain.Entities.WebBudgetIncome> GetIncomeByEncodedName(string encodedIncomeName)
		=> await _webBudgetDbContext.WebBudgetIncome.FirstAsync(i => i.EncodedIncomeName == encodedIncomeName);

		public async Task CommitChanges()
		=> await _webBudgetDbContext.SaveChangesAsync();
	
	}
}

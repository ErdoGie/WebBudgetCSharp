using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.Services
{
    //odnoszę się do warstwy application aby przekierować logikę z solucji WebBudgetController
    // przez to będę stąd zarządzać moimi encjami WebBudget'a

    public class WebBudgetService : IWebBudgetService
    {
        public readonly IWebBudgetRepository _webBudgetRepository;

        // ponownie muszę przekazac referencje do mojego repozytorium przez konstruktor
        public WebBudgetService(IWebBudgetRepository webBudgetRepository)
        {
            _webBudgetRepository = webBudgetRepository;
        }
        public async Task CreateIncome(Domain.Entities.WebBudgetIncome webBudgetIncome)
        {

			//przed zapisem do bazy danych muszę wywołać moje metody,
			//aby były prawidłowo zapisane podczas powstawania nowej encji.

			webBudgetIncome.EncodeIncomeName();

            await _webBudgetRepository.CreateIncome(webBudgetIncome);
        }
        public async Task CreateExpense(Domain.Entities.WebBudgetExpense webBudgetExpense)
        {

			webBudgetExpense.EncodeExpenseName();

            await _webBudgetRepository.CreateExpense(webBudgetExpense);
        }
    }
}

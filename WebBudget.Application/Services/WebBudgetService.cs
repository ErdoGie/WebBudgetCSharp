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
        public async Task CreateIncome(Domain.Entities.WebBudget webBudget)
        {

            //przed zapisem do bazy danych muszę wywołać moje metody,
            //aby były prawidłowo zapisane podczas powstawania nowej encji.
            
            webBudget.BudgetIncome.EncodeIncomeName();

            await _webBudgetRepository.Create(webBudget);
        }
        public async Task CreateExpense(Domain.Entities.WebBudget webBudget)
        {

            webBudget.BudgetExpense.EncodeExpenseName();

            await _webBudgetRepository.Create(webBudget);
        }
    }
}

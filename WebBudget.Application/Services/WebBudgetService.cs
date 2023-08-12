using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget;
using WebBudget.Domain.Entities;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.Services
{
	//odnoszę się do warstwy application aby przekierować logikę z solucji WebBudgetController
	// przez to będę stąd zarządzać moimi encjami WebBudget'a

	public class WebBudgetService : IWebBudgetService
	{
		public readonly IWebBudgetRepository _webBudgetRepository;
		public readonly IMapper _mapper;

		// ponownie muszę przekazac referencje do mojego repozytorium przez konstruktor
		public WebBudgetService(IWebBudgetRepository webBudgetRepository, IMapper mapper)
		{
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
		}
		public async Task CreateExpense(WebBudgetExpenseDTO webBudgetExpenseDTO)
		{
			var webBudgetExpense = _mapper.Map<Domain.Entities.WebBudgetExpense>(webBudgetExpenseDTO);

			webBudgetExpense.EncodeExpenseName();

			await _webBudgetRepository.CreateExpense(webBudgetExpense);
		}
		public async Task CreateIncome(WebBudgetIncomeDTO webBudgetIncomeDTO)
		{
			var webBudgetIncome = _mapper.Map<Domain.Entities.WebBudgetIncome>(webBudgetIncomeDTO);
		
			webBudgetIncome.EncodeIncomeName(); 

			await _webBudgetRepository.CreateIncome(webBudgetIncome);
		}

		public async Task<IEnumerable<WebBudgetExpenseDTO>> GetAllExpenses()
		{
			var webBudgetExpense = await _webBudgetRepository.GetAllExpenses();

			var dtoExpenses = _mapper.Map<IEnumerable<WebBudgetExpenseDTO>>(webBudgetExpense);

			return dtoExpenses;

		}

		public async Task<IEnumerable<WebBudgetIncomeDTO>> GetAllIncomes()
		{
			var webBudgetIncome = await _webBudgetRepository.GetAllIncomes();

			var dtoIncomes = _mapper.Map<IEnumerable<WebBudgetIncomeDTO>>(webBudgetIncome);

			return dtoIncomes;
		}
	}
}

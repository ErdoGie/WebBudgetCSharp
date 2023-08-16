using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetAllWebBudgetExpenses
{
	public class GetAllWebBudgetExpenseQueryHandle : IRequestHandler<GetAllWebBudgetExpensesQuery, IEnumerable<WebBudgetExpenseDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IWebBudgetRepository _webBudgetRepository;

		public GetAllWebBudgetExpenseQueryHandle(IWebBudgetRepository webBudgetRepository, IMapper mapper)
		{
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<WebBudgetExpenseDTO>> Handle(GetAllWebBudgetExpensesQuery request, CancellationToken cancellationToken)
		{

			var webBudgetExpense = await _webBudgetRepository.GetAllExpenses();


			var dtoExpenses = _mapper.Map<IEnumerable<WebBudgetExpenseDTO>>(webBudgetExpense);

			return dtoExpenses;
		}
	}
}

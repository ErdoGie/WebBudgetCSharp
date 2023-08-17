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
	public class GetAllWebBudgetExpenseForLoggedUsersQueryHandle : IRequestHandler<GetAllWebBudgetExpensesForLoggedusersQuery, IEnumerable<WebBudgetExpenseDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IUserContext _userContext;

		public GetAllWebBudgetExpenseForLoggedUsersQueryHandle(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
		{
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
			_userContext = userContext;
		}

		public async Task<IEnumerable<WebBudgetExpenseDTO>> Handle(GetAllWebBudgetExpensesForLoggedusersQuery request, CancellationToken cancellationToken)
		{
			var currentlyLoggedUser = _userContext.GetCurrentlyLoggedUser();
			var userId = currentlyLoggedUser?.Id;


			var webBudgetExpense = await _webBudgetRepository.GetAllExpensesForLoggedUser(userId!);


			var dtoExpenses = _mapper.Map<IEnumerable<WebBudgetExpenseDTO>>(webBudgetExpense);

			return dtoExpenses;
		}
	}
}

using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.CreateExpenseViewModel
{
	public class ExpenseViewModelCommandHandler : IRequestHandler<ExpenseViewModelCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;


		public ExpenseViewModelCommandHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
		{

			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
			_userContext = userContext;

		}
		public async Task<Unit> Handle(ExpenseViewModelCommand request, CancellationToken cancellationToken)
		{
			var userId = _userContext.GetCurrentlyLoggedUser()!.Id;

			foreach (var expenseCategory in request.ExpenseCategories)
			{
				var domainExpenseCategory = _mapper.Map<Domain.Entities.ExpenseCategory>(expenseCategory);
				domainExpenseCategory.UserId = userId;
				await _webBudgetRepository.AddExpenseCategory(domainExpenseCategory);
			}

			var domainExpense = _mapper.Map<Domain.Entities.WebBudgetExpense>(request.ExpenseCommand);
			domainExpense.CreatedById = userId;
			domainExpense.EncodeExpenseName();

			await _webBudgetRepository.CreateExpense(domainExpense);

			return Unit.Value;
		}
	}
}

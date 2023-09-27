using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.CreateExpenseCategory
{
	public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand>
	{

		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;

		public CreateExpenseCategoryCommandHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
		{

			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
			_userContext = userContext;

		}

		public async Task<Unit> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
		{

			var expenseCategory = _mapper.Map<Domain.Entities.ExpenseCategory>(request);

			expenseCategory.CategoryName = request.CategoryName;
			expenseCategory.Limit = request.Limit;

			if (expenseCategory.Limit > 0)
			{
				expenseCategory.IsLimitSet = true;
			}
			else
			{
				expenseCategory.IsLimitSet = false;
			}

			expenseCategory.UserId = _userContext.GetCurrentlyLoggedUser()!.Id;


			await _webBudgetRepository.AddExpenseCategory(expenseCategory);

			return Unit.Value;
		}
	}
}

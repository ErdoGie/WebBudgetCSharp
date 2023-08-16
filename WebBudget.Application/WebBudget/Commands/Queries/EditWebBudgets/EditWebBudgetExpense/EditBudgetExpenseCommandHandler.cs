using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Entities;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense
{
	public class EditBudgetExpenseCommandHandler : IRequestHandler<EditWebBudgetExpenseCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IUserContext _userContext;


        public EditBudgetExpenseCommandHandler(IWebBudgetRepository webBudgetRepository, IUserContext userContext)
        {
			_webBudgetRepository = webBudgetRepository;
			_userContext = userContext;
        }

        public async Task<Unit> Handle(EditWebBudgetExpenseCommand request, CancellationToken cancellationToken)
		{

			var webBudget = await _webBudgetRepository.GetExpenseByEncodedName(request.EncodedExpenseName);

            var user = _userContext.GetCurrentlyLoggedUser();
            var hasAccess = user != null && webBudget.CreatedById == user.Id;

			if (!hasAccess)
			{
				return Unit.Value;
			}


            webBudget.ExpenseType = request.ExpenseType;
			webBudget.ExpenseValue = request.ExpenseValue;
			webBudget.ExpenseDate = request.ExpenseDate;
			webBudget.EncodedExpenseName = request.EncodedExpenseName!;

			webBudget.EncodeExpenseName();
			request.EncodeExpenseName();

			await _webBudgetRepository.CommitChanges();

			return Unit.Value;

		}
	}
}

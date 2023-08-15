using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense
{
	public class EditBudgetExpenseCommandHandler : IRequestHandler<EditWebBudgetExpenseCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;

        public EditBudgetExpenseCommandHandler(IWebBudgetRepository webBudgetRepository)
        {
			_webBudgetRepository = webBudgetRepository;
        }

        public async Task<Unit> Handle(EditWebBudgetExpenseCommand request, CancellationToken cancellationToken)
		{

			var webBudget = await _webBudgetRepository.GetExpenseByEncodedName(request.EncodedExpenseName);

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

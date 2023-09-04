using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetExpense
{
	public class DeleteWebBudgetExpenseCommandHandler :IRequestHandler<DeleteWebBudgetExpenseCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;

        public DeleteWebBudgetExpenseCommandHandler(IWebBudgetRepository webBudgetRepository)
        {
            _webBudgetRepository = webBudgetRepository;

        }

        public async Task<Unit> Handle(DeleteWebBudgetExpenseCommand request, CancellationToken cancellationToken)
		{

			var webBudget = await _webBudgetRepository.GetExpenseById(request.ExpenseId);

			await _webBudgetRepository.RemoveExpense(webBudget.ExpenseId);

			await _webBudgetRepository.CommitChanges();

			return Unit.Value;

		}

	}
}

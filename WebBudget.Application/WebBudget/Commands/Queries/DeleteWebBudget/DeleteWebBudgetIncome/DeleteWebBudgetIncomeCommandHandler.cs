using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetIncome
{
	public class DeleteWebBudgetIncomeCommandHandler : IRequestHandler<DeleteWebBudgetIncomeeCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
        public DeleteWebBudgetIncomeCommandHandler(IWebBudgetRepository webBudgetRepository)
        {
            _webBudgetRepository = webBudgetRepository;

        }

        public async Task<Unit> Handle(DeleteWebBudgetIncomeeCommand request, CancellationToken cancellationToken)
		{
			var webBudget = await _webBudgetRepository.GetIncomeByIncomeId(request.IncomeId!);

			await _webBudgetRepository.RemoveIncome(webBudget.IncomeId);

			await _webBudgetRepository.CommitChanges();

			return Unit.Value;

		}
	}
}

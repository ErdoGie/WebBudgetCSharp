using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetIncome
{
	public class EditWebBudgetIncomeCommandHandler : IRequestHandler<EditWebBudgetIncomeCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IUserContext _userContext;

		public EditWebBudgetIncomeCommandHandler(IWebBudgetRepository webBudgetRepository, IUserContext userContext)
		{
			_webBudgetRepository = webBudgetRepository;
			_userContext = userContext;
		}
		public async Task<Unit> Handle(EditWebBudgetIncomeCommand request, CancellationToken cancellationToken)
		{
			var webBudget = await _webBudgetRepository.GetIncomeByIncomeId(request.IncomeId!);
			var user = _userContext.GetCurrentlyLoggedUser();
			var hasAccess = user != null && webBudget.CreatedById == user.Id;

			if (!hasAccess)
			{
				return Unit.Value;
			}
			// mam porbrana encje z DB - musze przekształcic ją od usera aplikacji

			webBudget.IncomeValue = request.IncomeValue;
			webBudget.IncomeType = request.IncomeType;
			webBudget.IncomeDate = request.IncomeDate;
			webBudget.EncodedIncomeName = request.EncodedIncomeName!;

			// najpierw zapisuje metode EncodeIncomeName dla webBUdget, a następnie dla requesta
			// inaczej nie będzie widzieć pierwotnie zapisanej metody.
			webBudget.EncodeIncomeName();
			request.EncodeIncomeName();


			//normalnie powinienem tu już zapisać lepiej w repozytorium interfejsu dodać kolejną metode ktora będzie zapisywała zmiany

			await _webBudgetRepository.CommitChanges();

			return Unit.Value;
		}
	}
}

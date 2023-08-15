using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetIncome
{
    public class EditWebBudgetIncomeCommandHandler : IRequestHandler<EditWebBudgetIncomeCommand>
    {
        private readonly IWebBudgetRepository _webBudgetRepository;
        public EditWebBudgetIncomeCommandHandler(IWebBudgetRepository webBudgetRepository)
        {
            _webBudgetRepository = webBudgetRepository;
        }
        public async Task<Unit> Handle(EditWebBudgetIncomeCommand request, CancellationToken cancellationToken)
        {
            var webBudget = await _webBudgetRepository.GetIncomeByEncodedName(request.EncodedIncomeName!);

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

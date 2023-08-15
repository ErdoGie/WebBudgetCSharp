using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense
{
	public class CreateWebBudgetExpenseCommandHandler : IRequestHandler<CreateWebBudgetExpenseCommand>
	{
		private readonly IMapper _mapper;
		private readonly IWebBudgetRepository _webBudgetRepository;

        public CreateWebBudgetExpenseCommandHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper)
        {
            _webBudgetRepository = webBudgetRepository;
			_mapper = mapper;

        }


        public async Task<Unit> Handle(CreateWebBudgetExpenseCommand request, CancellationToken cancellationToken)
		{
			var webBudgetExpense = _mapper.Map<Domain.Entities.WebBudgetExpense>(request);

			webBudgetExpense.EncodeExpenseName();

			await _webBudgetRepository.CreateExpense(webBudgetExpense);
		
			return Unit.Value;
		}
	}
}

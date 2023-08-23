using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome
{
	public class CreateWebBudgetIncomeCOmmandHandler : IRequestHandler<CreateWebBudgetIncomeCommand>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;

        public CreateWebBudgetIncomeCOmmandHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
        {
            
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
			_userContext = userContext;

        }

        public async Task<Unit> Handle(CreateWebBudgetIncomeCommand request, CancellationToken cancellationToken)
		{
			var webBudgetIncome = _mapper.Map<Domain.Entities.WebBudgetIncome>(request);

			webBudgetIncome.EncodeIncomeName();

			webBudgetIncome.CreatedById = _userContext.GetCurrentlyLoggedUser()!.Id;
			

			await _webBudgetRepository.CreateIncome(webBudgetIncome);


			return Unit.Value;
		}
	}
}

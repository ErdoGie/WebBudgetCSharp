using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Entities;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense
{
    public class CreateWebBudgetExpenseCommandHandler : IRequestHandler<CreateWebBudgetExpenseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWebBudgetRepository _webBudgetRepository;
        private readonly IUserContext _userContext;

        public CreateWebBudgetExpenseCommandHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
        {
            _webBudgetRepository = webBudgetRepository;
            _mapper = mapper;
            _userContext = userContext;

        }

        public async Task<Unit> Handle(CreateWebBudgetExpenseCommand request, CancellationToken cancellationToken)
        {
            var webBudgetExpense = _mapper.Map<Domain.Entities.WebBudgetExpense>(request);

            webBudgetExpense.CreatedById = _userContext.GetCurrentlyLoggedUser()!.Id;

            await _webBudgetRepository.CreateExpense(webBudgetExpense);

            return Unit.Value;
        }
    }
}

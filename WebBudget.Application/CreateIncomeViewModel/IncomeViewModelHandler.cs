using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.CreateIncomeViewModel
{
    public class IncomeViewModelHandler : IRequestHandler<IncomeViewModel>
    {
        private readonly IWebBudgetRepository _webBudgetRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public IncomeViewModelHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
        {

            _webBudgetRepository = webBudgetRepository;
            _mapper = mapper;
            _userContext = userContext;

        }
        public async Task<Unit> Handle(IncomeViewModel request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentlyLoggedUser().Id;

            // Dodawanie kategorii
            foreach (var incomeCategory in request.IncomeCategories)
            {
                var domainIncomeCategory = _mapper.Map<Domain.Entities.IncomeCategory>(incomeCategory);
                domainIncomeCategory.UserId = userId;
                await _webBudgetRepository.AddIncomeCategory(domainIncomeCategory);
            }

            // Dodawanie przychodu
            var domainIncome = _mapper.Map<Domain.Entities.WebBudgetIncome>(request.IncomeCommand);
            domainIncome.CreatedById = userId;
            domainIncome.EncodeIncomeName();

            await _webBudgetRepository.CreateIncome(domainIncome);

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.CreateIncomeCategory
{
    public class CreateIncomeCategoryCommandHandler : IRequestHandler<CreateIncomeCategoryCommand>
    {
        private readonly IWebBudgetRepository _webBudgetRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateIncomeCategoryCommandHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
        {

            _webBudgetRepository = webBudgetRepository;
            _mapper = mapper;
            _userContext = userContext;

        }

		public async Task<Unit> Handle(CreateIncomeCategoryCommand request, CancellationToken cancellationToken)
		{
			var existingCategory = await _webBudgetRepository.GetAllIncomeCategoriesForUser(request.CategoryName);
			

			var incomeCategory = _mapper.Map<Domain.Entities.IncomeCategory>(request);
			incomeCategory.CategoryName = request.CategoryName;
			incomeCategory.UserId = _userContext.GetCurrentlyLoggedUser()!.Id;

			await _webBudgetRepository.AddIncomeCategory(incomeCategory);

			return Unit.Value;
		}
	}

}


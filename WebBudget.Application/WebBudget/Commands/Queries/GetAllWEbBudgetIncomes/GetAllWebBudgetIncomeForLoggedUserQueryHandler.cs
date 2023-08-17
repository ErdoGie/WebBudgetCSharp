using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetAllWEbBudgetIncomes
{
	public class GetAllWebBudgetIncomeForLoggedUserQueryHandler : IRequestHandler<GetAllWebBudgetIncomesForLoggedUserQuery, IEnumerable<WebBudgetIncomeDTO>>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;
		private readonly IUserContext _userContext;


		public GetAllWebBudgetIncomeForLoggedUserQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, IUserContext userContext)
        {
            
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
			_userContext = userContext;

        }

        public async Task<IEnumerable<WebBudgetIncomeDTO>> Handle(GetAllWebBudgetIncomesForLoggedUserQuery request, CancellationToken cancellationToken)
		{
			var currentlyLoggedUser = _userContext.GetCurrentlyLoggedUser();
			var userID = currentlyLoggedUser?.Id;


			var webBudgetIncome = await _webBudgetRepository.GetAllIncomesForLoggedUser(userID!);

			var dtoIncomes = _mapper.Map<IEnumerable<WebBudgetIncomeDTO>>(webBudgetIncome);

			return dtoIncomes;

		}
	}
}

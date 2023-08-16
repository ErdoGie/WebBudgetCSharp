using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.GetWebBudgetByEncodedNameExpense
{
	public class GetWebBudgetExpenseByEncodedNameQueryHandler : IRequestHandler<GetWebBudgetExpenseByEncodedNameQuery, WebBudgetExpenseDTO>
	{

		
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;


        public GetWebBudgetExpenseByEncodedNameQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper, UserManager<IdentityUser> userManager )
		{
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
			_userManager = userManager;

		}
		public async Task<WebBudgetExpenseDTO> Handle(GetWebBudgetExpenseByEncodedNameQuery request, CancellationToken cancellationToken)
		{
			var webBudget = await _webBudgetRepository.GetExpenseByEncodedName(request.EncodedName);



			var dto = _mapper.Map<WebBudgetExpenseDTO>(webBudget);

			return dto;
		}
	}
}

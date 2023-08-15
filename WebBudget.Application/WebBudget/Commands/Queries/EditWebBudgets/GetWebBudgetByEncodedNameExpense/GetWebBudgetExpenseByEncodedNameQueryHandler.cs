using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.GetWebBudgetByEncodedNameExpense
{
	public class GetWebBudgetExpenseByEncodedNameQueryHandler : IRequestHandler<GetWebBudgetExpenseByEncodedNameQuery, WebBudgetExpenseDTO>
	{

		
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;

		public GetWebBudgetExpenseByEncodedNameQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper)
		{
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;

		}
		public async Task<WebBudgetExpenseDTO> Handle(GetWebBudgetExpenseByEncodedNameQuery request, CancellationToken cancellationToken)
		{
			var webBudget = await _webBudgetRepository.GetExpenseByEncodedName(request.EncodedName);

			var dto = _mapper.Map<WebBudgetExpenseDTO>(webBudget);

			return dto;
		}
	}
}

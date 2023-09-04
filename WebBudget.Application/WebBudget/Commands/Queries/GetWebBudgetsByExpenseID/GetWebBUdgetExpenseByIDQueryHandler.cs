using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByEncodedName;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByExpenseID
{
	public class GetWebBudgetExpenseByIDQueryHandler : IRequestHandler<GetWebBudgetExpenseByIDQuery, WebBudgetExpenseDTO>
	{

		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;
		public GetWebBudgetExpenseByIDQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper)
		{
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;
		}

		public async Task<WebBudgetExpenseDTO> Handle(GetWebBudgetExpenseByIDQuery request, CancellationToken cancellationToken)
		{
			var webBudget = await _webBudgetRepository.GetExpenseById(request.ExpenseId);

			var dto = _mapper.Map<WebBudgetExpenseDTO>(webBudget);

			return dto;
		}
	}
}

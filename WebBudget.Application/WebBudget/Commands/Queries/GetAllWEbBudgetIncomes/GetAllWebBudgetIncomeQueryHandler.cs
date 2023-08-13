using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetAllWEbBudgetIncomes
{
	public class GetAllWebBudgetIncomeQueryHandler : IRequestHandler<GetAllWebBudgetIncomesQuery, IEnumerable<WebBudgetIncomeDTO>>
	{
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly IMapper _mapper;


		public GetAllWebBudgetIncomeQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper)
        {
            
			_webBudgetRepository = webBudgetRepository;
			_mapper = mapper;

        }

        public async Task<IEnumerable<WebBudgetIncomeDTO>> Handle(GetAllWebBudgetIncomesQuery request, CancellationToken cancellationToken)
		{

			var webBudgetIncome = await _webBudgetRepository.GetAllIncomes();

			var dtoIncomes = _mapper.Map<IEnumerable<WebBudgetIncomeDTO>>(webBudgetIncome);

			return dtoIncomes;

		}
	}
}

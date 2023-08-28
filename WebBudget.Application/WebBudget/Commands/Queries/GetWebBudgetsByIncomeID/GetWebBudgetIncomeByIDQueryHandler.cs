using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.UserApplication;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByEncodedName
{
    public class GetWebBudgetIncomeByIDQueryHandler : IRequestHandler<GetWebBudgetIncomeByIDQuery, WebBudgetIncomeDTO>
    {

        private readonly IWebBudgetRepository _webBudgetRepository;
        private readonly IMapper _mapper;
        public GetWebBudgetIncomeByIDQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper )
        {
            _webBudgetRepository = webBudgetRepository;
            _mapper = mapper;
        }

        public async Task<WebBudgetIncomeDTO> Handle(GetWebBudgetIncomeByIDQuery request, CancellationToken cancellationToken)
        {

            var webBudget = await _webBudgetRepository.GetIncomeByIncomeId(request.IncomeId);

		
			// musze posluzyc sie mapperem zeby przeksztalcic encje bazodanowa na obiekt DTO

			var dto = _mapper.Map<WebBudgetIncomeDTO>(webBudget);

            return dto;
        }
    }
}

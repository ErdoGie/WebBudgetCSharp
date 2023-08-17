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
    public class GetWebBudgetIncomeByEncodedNameQueryHandler : IRequestHandler<GetWebBudgetIncomeByEncodedNameQuery, WebBudgetIncomeDTO>
    {

        private readonly IWebBudgetRepository _webBudgetRepository;
        private readonly IMapper _mapper;
        public GetWebBudgetIncomeByEncodedNameQueryHandler(IWebBudgetRepository webBudgetRepository, IMapper mapper )
        {
            _webBudgetRepository = webBudgetRepository;
            _mapper = mapper;
        }

        public async Task<WebBudgetIncomeDTO> Handle(GetWebBudgetIncomeByEncodedNameQuery request, CancellationToken cancellationToken)
        {

            var webBudget = await _webBudgetRepository.GetIncomeByEncodedName(request.EncodedIncomeName);

		
			// musze posluzyc sie mapperem zeby przeksztalcic encje bazodanowa na obiekt DTO

			var dto = _mapper.Map<WebBudgetIncomeDTO>(webBudget);

            return dto;
        }
    }
}

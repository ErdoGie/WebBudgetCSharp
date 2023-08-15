using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByEncodedName
{
    public class GetWebBudgetIncomeByEncodedNameQuery : IRequest<WebBudgetIncomeDTO>
    {
        public string EncodedIncomeName { get; set; }

        public GetWebBudgetIncomeByEncodedNameQuery(string encodedIncomeName)
        {

            EncodedIncomeName = encodedIncomeName;
        }
    }
}

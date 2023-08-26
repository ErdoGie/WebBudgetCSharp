using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByEncodedName
{
    public class GetWebBudgetIncomeByIDQuery : IRequest<WebBudgetIncomeDTO>
    {
        public int IncomeId  { get; set; }

        public GetWebBudgetIncomeByIDQuery(int incomeId)
        {

            IncomeId = incomeId;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.GetWebBudgetByEncodedNameExpense
{
	public class GetWebBudgetExpenseByEncodedNameQuery :IRequest<WebBudgetExpenseDTO>
	{
		public string EncodedName { get; set; }

        public GetWebBudgetExpenseByEncodedNameQuery(string encodedExpenseName)
        {
            EncodedName = encodedExpenseName;
            
        }
    }
}

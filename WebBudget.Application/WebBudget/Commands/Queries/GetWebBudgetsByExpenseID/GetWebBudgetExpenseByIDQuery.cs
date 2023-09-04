using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByExpenseID
{
	public class GetWebBudgetExpenseByIDQuery : IRequest<WebBudgetExpenseDTO>
	{

		public int ExpenseId { get; set; }

		public GetWebBudgetExpenseByIDQuery(int expenseId)
		{

			ExpenseId = expenseId;
		}

	}
}

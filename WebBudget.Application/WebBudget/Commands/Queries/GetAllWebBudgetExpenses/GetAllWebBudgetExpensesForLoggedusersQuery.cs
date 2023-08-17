using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetAllWebBudgetExpenses
{
    public class GetAllWebBudgetExpensesForLoggedusersQuery : IRequest<IEnumerable<WebBudgetExpenseDTO>>
    {
        public string UserId { get;  }

        public GetAllWebBudgetExpensesForLoggedusersQuery(string userId)
        {
            UserId = userId;

        }


    }
}

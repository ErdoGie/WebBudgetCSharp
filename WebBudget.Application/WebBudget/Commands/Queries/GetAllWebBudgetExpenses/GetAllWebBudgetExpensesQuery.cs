﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetAllWebBudgetExpenses
{
	public class GetAllWebBudgetExpensesQuery : IRequest<IEnumerable<WebBudgetExpenseDTO>>
	{



	}
}

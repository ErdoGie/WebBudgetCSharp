﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget.Commands.Queries.GetAllWEbBudgetIncomes
{
	// w tej kwerendzie chce zwrocic liste obiektow typu incomeDTO
	public class GetAllWebBudgetIncomesForLoggedUserQuery :IRequest<IEnumerable<WebBudgetIncomeDTO>>
	{
		public string UserId { get; }

        public GetAllWebBudgetIncomesForLoggedUserQuery(string userId)
        {
            UserId = userId;
            
        }
    }
}

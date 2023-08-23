using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Application.WebBudget.DeleteIncomeCategory
{
	public class DeleteIncomeCategoryCommandHandler 
	{
		private readonly IWebBudgetRepository _webBudgetRepository;

        public DeleteIncomeCategoryCommandHandler(IWebBudgetRepository webBudgetRepository)
        {
            
			_webBudgetRepository = webBudgetRepository;

        }

	

	}

	
}

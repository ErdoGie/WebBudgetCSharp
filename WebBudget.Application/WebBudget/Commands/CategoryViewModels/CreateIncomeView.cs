using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel;

namespace WebBudget.Application.WebBudget.Commands.CategoryViewModels
{
    public class CreateIncomeView
    {
        public IEnumerable<WebBudgetIncomeDTO> Incomes { get; set; } = new List<WebBudgetIncomeDTO>();
        public IncomeViewModelCommand IncomeCommand { get; set; } = new IncomeViewModelCommand();


    }
}

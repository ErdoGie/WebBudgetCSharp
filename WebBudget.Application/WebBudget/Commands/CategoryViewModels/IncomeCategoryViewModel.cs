using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Domain.Entities;

namespace WebBudget.Application.WebBudget.Commands.CategoryViewModels
{
    public class IncomeCategoryViewModel
    {
        public IEnumerable<IncomeCategory> IncomeCategories { get; set; } = new List<IncomeCategory>();
        public CreateIncomeCategoryCommand CreateIncomeCategoryCommand { get; set; } = new CreateIncomeCategoryCommand();



    }
}

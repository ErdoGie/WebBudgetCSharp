using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget
{
    public class ExpenseCategoryDTO
    {
        public string CategoryName { get; set; } = default!;
        public float? Limit { get; set; } = default!;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBudget.Domain.Interfaces;

namespace WebBudget.Domain.Entities
{
    public class CalcualteBalance
    {
        private readonly IWebBudgetRepository _webBudgetRepository;

        public CalcualteBalance(IWebBudgetRepository webBudgetRepository)
        {
            _webBudgetRepository = webBudgetRepository;
        }


        public async Task<float> CalculateUsersBalance(string userId, DateTime beginningDate, DateTime endingDate)
        {
            var incomes = await _webBudgetRepository.GetAllUserIncomesFromDateRange(userId, beginningDate, endingDate);
            var expenses = await _webBudgetRepository.GetAllUserExpensesFromDateRange(userId, beginningDate, endingDate);

            float totalIncomes = incomes.Sum(i => i.IncomeValue);
            float totalExpenses = expenses.Sum(e => e.ExpenseValue);

            float balance = totalIncomes - totalExpenses;

            return balance;
        }


    }
}

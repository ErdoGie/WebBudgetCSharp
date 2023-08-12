using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.Services;
using WebBudget.Application.WebBudget;
using WebBudget.Domain.Entities;
using X.PagedList;
using static Azure.Core.HttpHeader;
//
namespace WebBudget.MVC.Controllers
{
	public class WebBudgetController : Controller
	{
		public readonly IWebBudgetService _webBudgetService;

		//przekazuje zależność 
		public WebBudgetController(IWebBudgetService webBudgetService)
		{

			_webBudgetService = webBudgetService;
		}

		// do tej metody przyjmuję dany typ budzetu
		[HttpPost]
		public async Task<IActionResult> CreateIncome(WebBudgetIncomeDTO webBudgetIncome)
		{
			if (!ModelState.IsValid)
			{
				return View(webBudgetIncome);
			}

			await _webBudgetService.CreateIncome(webBudgetIncome);

			TempData["IncomeAdded"] = true;

			return RedirectToAction(nameof(IncomesIndex)); 
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpense(WebBudgetExpenseDTO webBudgetExpense)
		{
			if (!ModelState.IsValid)
			{
				return View(webBudgetExpense);
			}

			await _webBudgetService.CreateExpense(webBudgetExpense);

			return RedirectToAction(nameof(ExpensesIndex));
		}
        public async Task<IActionResult> IncomesIndex(int? page)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;

            var webBudgetIncome = await _webBudgetService.GetAllIncomes();

            var paginatedIncomeData = webBudgetIncome.ToPagedList(pageNumber, pageSize);

            int pageCount = (int)Math.Ceiling((double)webBudgetIncome.Count() / pageSize);

            ViewBag.PageCount = pageCount;

            return View(paginatedIncomeData);
        }

		public async Task<IActionResult> ExpensesIndex(int? page)
		{
			int pageSize = 6;
			int pageNumber = page ?? 1;
			var webBudgetExpese = await _webBudgetService.GetAllExpenses();

			var paginatedExpenseData = webBudgetExpese.ToPagedList(pageNumber, pageSize);

			int pageCount = (int)Math.Ceiling((double)webBudgetExpese.Count() / pageSize);

			ViewBag.PageCount = pageCount;

			return View(paginatedExpenseData);
		}


        public IActionResult CreateExpense()
		{
			return View();
		}

		public IActionResult CreateIncome()
		{


			return View();
		}
	}
}

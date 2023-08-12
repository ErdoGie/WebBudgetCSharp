using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.Services;
using WebBudget.Application.WebBudget;
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

			return RedirectToAction("Index", "Home"); 
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpense(WebBudgetExpenseDTO webBudgetExpense)
		{
			if (!ModelState.IsValid)
			{
				return View(webBudgetExpense);
			}

			await _webBudgetService.CreateExpense(webBudgetExpense);

			return RedirectToAction(nameof(CreateExpense));
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

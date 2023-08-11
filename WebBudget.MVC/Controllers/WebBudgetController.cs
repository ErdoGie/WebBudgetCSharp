using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.Services;
using WebBudget.Application.WebBudget;
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

			return RedirectToAction(nameof(CreateIncome));
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpense(WebBudgetExpenseDTO webBudgetExpense)
		{
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

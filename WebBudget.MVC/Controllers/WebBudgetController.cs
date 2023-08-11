using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.Services;
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
		public async Task<IActionResult> CreateIncome(Domain.Entities.WebBudget webBudget)
		{
			await _webBudgetService.CreateIncome(webBudget);

			return RedirectToAction(nameof(CreateIncome));
			// wrócić muszę z refactorem tutaj.
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpense(Domain.Entities.WebBudget webBudget)
		{
			await _webBudgetService.CreateExpense(webBudget);

			return RedirectToAction(nameof(CreateExpense));
			// wrócić muszę z refactorem tutaj.
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

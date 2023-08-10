using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.Services;

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
		public async Task <IActionResult> Create(Domain.Entities.WebBudget webBudget)
		{
			await _webBudgetService.Create(webBudget);

			return RedirectToAction(nameof(Create));
			// wrócić muszę z refactorem tutaj.
		}
	}
}

using AutoMapper;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.GetWebBudgetByEncodedNameExpense;
using WebBudget.Application.WebBudget.Commands.Queries.GetAllWebBudgetExpenses;
using WebBudget.Application.WebBudget.Commands.Queries.GetAllWEbBudgetIncomes;
using WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByEncodedName;
using WebBudget.Domain.Entities;
using X.PagedList;
//
namespace WebBudget.MVC.Controllers
{
	public class WebBudgetController : Controller
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;

		//przekazuje zależność 
		public WebBudgetController(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager)
		{
			_mapper = mapper;
			_mediator = mediator;
			_userManager = userManager;
		}
		// ------------------------------------------------- CREATE INCOME --------------------------------------------- //

		// do tej metody przyjmuję dany typ budzetu
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateIncome(CreateWebBudgetIncomeCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			TempData["IncomeAdded"] = true;

			return RedirectToAction(nameof(IncomesIndex));
		}

		[Authorize]
		public IActionResult CreateIncome()
		{
			return View();
		}
		// ------------------------------------------------- CREATE EXPENSE --------------------------------------------- //

		[Authorize]
		public IActionResult CreateExpense()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateExpense(CreateWebBudgetExpenseCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(ExpensesIndex));
		}
		// ------------------------------------------------- INDEXES--------------------------------------------- //

		public async Task<IActionResult> IncomesIndex(int? page, string userId)
		{
			int pageSize = 6;
			int pageNumber = page ?? 1;
			if (User.Identity!.IsAuthenticated)
			{
				var webBudgetIncomeQuery = new GetAllWebBudgetIncomesForLoggedUserQuery(userId);
				var webBudgetIncome = await _mediator.Send(webBudgetIncomeQuery);

				var paginatedIncomeData = webBudgetIncome.ToPagedList(pageNumber, pageSize);

				int pageCount = (int)Math.Ceiling((double)webBudgetIncome.Count() / pageSize);

				ViewBag.PageCount = pageCount;

				return View(paginatedIncomeData);
			}

			else
			{
				return RedirectToAction("NoAccess", "Home");
			}
		}

		public async Task<IActionResult> ExpensesIndex(int? page, string userId)
		{
			int pageSize = 6;
			int pageNumber = page ?? 1;

			if (User.Identity!.IsAuthenticated)
			{

				var webBudgetExpeseQuery = new GetAllWebBudgetExpensesForLoggedusersQuery(userId);
				var webBudgetExpense = await _mediator.Send(webBudgetExpeseQuery);

				var paginatedExpenseData = webBudgetExpense.ToPagedList(pageNumber, pageSize);

				int pageCount = (int)Math.Ceiling((double)webBudgetExpense.Count() / pageSize);

				ViewBag.PageCount = pageCount;

				return View(paginatedExpenseData);
			}

			else
			{
				return RedirectToAction("NoAccess", "Home");
			}


		}
		// ------------------------------------------------- EDIT INCOME --------------------------------------------- //

		[Route("WebBudget/Income/{encodedIncomeName}/Edit")]
		public async Task<IActionResult> IncomeEdit(string encodedIncomeName)
		{
			var dto = await _mediator.Send(new GetWebBudgetIncomeByEncodedNameQuery(encodedIncomeName));

			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

			EditWebBudgetIncomeCommand model = _mapper.Map<EditWebBudgetIncomeCommand>(dto);

			return View(model);
		}

		[HttpPost]
		[Route("WebBudget/Income/{encodedIncomeName}/Edit")]
		public async Task<IActionResult> IncomeEdit(string encodedIncomeName, EditWebBudgetIncomeCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			TempData["IncomeAdded"] = true;

			return RedirectToAction(nameof(IncomesIndex));
		}
		// ---------------------------------------- EDIT EXPENSE -------------------------------------------------- //


		[Route("WebBudget/Expense/{encodedExpenseName}/Edit")]
		public async Task<IActionResult> ExpenseEdit(string encodedExpenseName)
		{
			var dto = await _mediator.Send(new GetWebBudgetExpenseByEncodedNameQuery(encodedExpenseName));

			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

			EditWebBudgetExpenseCommand model = _mapper.Map<EditWebBudgetExpenseCommand>(dto);

			return View(model);
		}

		[HttpPost]
		[Route("WebBudget/Expense/{encodedExpenseName}/Edit")]
		public async Task<IActionResult> ExpenseEdit(string encodedExpenseName, EditWebBudgetExpenseCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			TempData["IncomeAdded"] = true;

			return RedirectToAction(nameof(ExpensesIndex));
		}

		// ---------------------------------------- DELETE EXPENSE -------------------------------------------------- //

		[Route("WebBudget/Expense/{encodedExpenseName}/Delete")]
		public async Task<IActionResult> ExpenseDelete(string encodedExpenseName)
		{
			var dto = await _mediator.Send(new GetWebBudgetExpenseByEncodedNameQuery(encodedExpenseName));

			DeleteWebBudgetExpenseCommand model = _mapper.Map<DeleteWebBudgetExpenseCommand>(dto);


			return View(model);
		}

		[HttpPost]
		[Route("WebBudget/Expense/{encodedExpenseName}/Delete")]
		public async Task<IActionResult> ExpenseDelete(string encodedExpenseName, DeleteWebBudgetExpenseCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			TempData["IncomeAdded"] = true;

			return RedirectToAction(nameof(ExpensesIndex));
		}

		// ---------------------------------------- DELETE INCOME -------------------------------------------------- //

		[Route("WebBudget/Income/{encodedIncomeName}/Delete")]
		public async Task<IActionResult> IncomeDelete(string encodedIncomeName)
		{
			var dto = await _mediator.Send(new GetWebBudgetIncomeByEncodedNameQuery(encodedIncomeName));

			DeleteWebBudgetIncomeeCommand model = _mapper.Map<DeleteWebBudgetIncomeeCommand>(dto);


			return View(model);
		}

		[HttpPost]
		[Route("WebBudget/Income/{encodedIncomeName}/Delete")]
		public async Task<IActionResult> IncomeDelete(string encodedIncomeName, DeleteWebBudgetIncomeeCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			TempData["IncomeAdded"] = true;

			return RedirectToAction(nameof(IncomesIndex));
		}



	}
}

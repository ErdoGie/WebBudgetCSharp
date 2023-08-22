using AutoMapper;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.CreateExpenseCategory;
using WebBudget.Application.WebBudget.Commands.CreateExpenseViewModel;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel;
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
using WebBudget.Domain.Interfaces;
using WebBudget.Infrastructure.Persistance;
using WebBudget.Infrastructure.Repositories;
using X.PagedList;
//
namespace WebBudget.MVC.Controllers
{
	public class WebBudgetController : Controller
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly CalcualteBalance _calculateBalance;
		private readonly IWebBudgetRepository _webBudgetRepository;

		//przekazuje zależność 
		public WebBudgetController(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager, CalcualteBalance calcualteBalance, IWebBudgetRepository webBudgetRepository)
		{
			_mapper = mapper;
			_mediator = mediator;
			_userManager = userManager;
			_calculateBalance = calcualteBalance;
			_webBudgetRepository = webBudgetRepository;
		}
		// ------------------------------------------------- CREATE INCOME --------------------------------------------- //

		// do tej metody przyjmuję dany typ budzetu
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateIncome(IncomeViewModelCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);

			var viewModel = new IncomeViewModelCommand
			{
				IncomeCategories = incomeCategories
			};

			int? categoryId = await _webBudgetRepository.GetIncomeCategoryIdByNameAsync(command.IncomeCommand.IncomeType);
			if (categoryId != null)
			{
				command.IncomeCommand.IncomeCategoryId = categoryId.Value;
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(IncomesIndex));
		}


		[HttpGet]
		[Authorize]
		public async Task<IActionResult> CreateIncome()
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);

			var viewModel = new IncomeViewModelCommand
			{
				IncomeCategories = incomeCategories
			};

			return View(viewModel);
		}
        // ------------------------------------------------- CREATE EXPENSE --------------------------------------------- //

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateExpense(ExpenseViewModelCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var userId = _userManager.GetUserId(User);
            var incomeCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!);

            var viewModel = new ExpenseViewModelCommand
            {

                ExpenseCategories = incomeCategories
            };

            int? categoryId = await _webBudgetRepository.GetExpenseCategoryIdByNameAsync(command.ExpenseCommand.ExpenseType);
            if (categoryId != null)
            {
                command.ExpenseCommand.ExpenseCategoryId = categoryId.Value;
            }

            await _mediator.Send(command);


            return RedirectToAction(nameof(ExpensesIndex));
        }



        [HttpGet]
		[Authorize]
		public async Task<IActionResult> CreateExpense()
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!);

			var viewModel = new ExpenseViewModelCommand
			{
				ExpenseCategories = incomeCategories
			};

			return View(viewModel);
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

			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

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
			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}
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

		// ---------------------------------------- CALCULATE BALANCE -------------------------------------------------- //

		[HttpGet]

		public async Task<IActionResult> CalculateBalance(string userId, DateTime startDate, DateTime endDate)
		{
			float balance = await _calculateBalance.CalculateUsersBalance(userId, startDate, endDate);

			ViewBag.Balance = balance;


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Balance(DateTime startDate, DateTime endDate)
		{
			string userId = _userManager.GetUserId(User)!;

			float balance = await _calculateBalance.CalculateUsersBalance(userId, startDate, endDate);

			ViewBag.Balance = balance;

			return View();
		}

		// ---------------------------------------- ADD INCOME CATEGORY -------------------------------------------------- //

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddIncomeCategory(CreateIncomeCategoryCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			var categoryName = command.CategoryName;
			var userId = _userManager.GetUserId(User);

			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);

			var existingCategory = incomeCategories.FirstOrDefault(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

			if (existingCategory != null)
			{
				ModelState.AddModelError("CategoryName", "Category with this name already exists.");
				return View(command);
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(ShowIncomeCategories));
		}

		[Authorize]
		public async Task<IActionResult> ShowIncomeCategories()
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);

			return View(incomeCategories);
		}

		public IActionResult AddIncomeCategory()
		{
			return View();
		}


		// ---------------------------------------- ADD EXPENSE CATEGORY -------------------------------------------------- //

		[Authorize]
		public async Task<IActionResult> ShowExpenseCategories()
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!);

			return View(incomeCategories);
		}


		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddExpenseCategory(CreateExpenseCategoryCommand command)
		{

			if (!ModelState.IsValid)
			{
				return View(command);
			}

            var categoryName = command.CategoryName;
            var userId = _userManager.GetUserId(User);

            var expenseCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!);

            var existingCategory = expenseCategories.FirstOrDefault(e => e.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

            if (existingCategory != null)
            {
                ModelState.AddModelError("CategoryName", "Category with this name already exists.");
                return View(command);
            }


            await _mediator.Send(command);

			return RedirectToAction(nameof(ShowExpenseCategories));
		}

		public IActionResult AddExpenseCategory()
		{
			return View();
		}


		public IActionResult ManageIncome()
		{
			return View();
		}

		public IActionResult ManageExpense()
		{
			return View();
		}


        // ---------------------------------------- DELETE INCOME CATEGORY -------------------------------------------------- //


        [HttpPost]
        public async Task<IActionResult> DeleteIncomeCategory(int categoryId)
        {
            await _webBudgetRepository.DeleteIncomeCategoryAndRelatedIncomesAsync(categoryId);

			return RedirectToAction(nameof(ManageIncome));
        }
		// ---------------------------------------- DELETE EXPENSE CATEGORY -------------------------------------------------- //


		[HttpPost]
		public async Task<IActionResult> DeleteExpenseCategory(int categoryId)
		{
			await _webBudgetRepository.DeleteExpenseCategoryAndRelateExpensesAsync(categoryId);

			return RedirectToAction(nameof(ManageExpense));
		}
	}
}

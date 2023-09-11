using AutoMapper;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBudget.Application.Balance;
using WebBudget.Application.WebBudget.Commands.CategoryViewModels;
using WebBudget.Application.WebBudget.Commands.CreateExpenseCategory;
using WebBudget.Application.WebBudget.Commands.CreateExpenseViewModel;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel;
using WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.Queries.DeleteWebBudget.DeleteWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets.EditWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.GetAllWebBudgetExpenses;
using WebBudget.Application.WebBudget.Commands.Queries.GetAllWEbBudgetIncomes;
using WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByEncodedName;
using WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetsByExpenseID;
using WebBudget.Domain.Entities;
using WebBudget.Domain.Interfaces;
using X.PagedList;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System.Web.Helpers;
using WebBudget.Application.Email;
using WebBudget.Infrastructure.Migrations;
//
namespace WebBudget.MVC.Controllers
{
	public class WebBudgetController : Controller
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IWebBudgetRepository _webBudgetRepository;
		private readonly SignInManager<IdentityUser> _signInManager;


		//przekazuje zależność 
		public WebBudgetController(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager, IWebBudgetRepository webBudgetRepository, SignInManager<IdentityUser> signInManager)
		{
			_mapper = mapper;
			_mediator = mediator;
			_userManager = userManager;
			_webBudgetRepository = webBudgetRepository;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{

			return View();
		}

		public IActionResult NoAccess()
		{

			return View();
		}




		// ------------------------------------------------- CREATE INCOME --------------------------------------------- //

		// do tej metody przyjmuję dany typ budzetu
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateIncome(IncomeViewModelCommand command)
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);


			if (!ModelState.IsValid)
			{

				command.IncomeCategories = incomeCategories;

				return RedirectToAction(nameof(IncomesIndex2));
			}

			var viewModel = new IncomeViewModelCommand
			{
				IncomeCategories = incomeCategories,
				IncomeCommand = command.IncomeCommand
			};

			int? categoryId = await _webBudgetRepository.GetIncomeCategoryIdByNameAsync(command.IncomeCommand.IncomeType);
			if (categoryId != null)
			{
				command.IncomeCommand.IncomeCategoryId = categoryId.Value;
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(IncomesIndex2));
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

		public async Task<IActionResult> IncomesIndex2()
		{
			var userId = _userManager.GetUserId(User);

			if (User.Identity!.IsAuthenticated)
			{
				var webBudgetIncomeQuery = new GetAllWebBudgetIncomesForLoggedUserQuery(userId!);
				var webBudgetIncome = await _mediator.Send(webBudgetIncomeQuery);

				var createIncomeView = new CreateIncomeView
				{
					Incomes = webBudgetIncome,
					IncomeCommand = new IncomeViewModelCommand
					{
						IncomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!)
					}
				};


				return View(createIncomeView);
			}
			else
			{
				return RedirectToAction("NoAccess", "Home");
			}
		}


		public async Task<IActionResult> ExpensesIndex()
		{
			var userId = _userManager.GetUserId(User);


			if (User.Identity!.IsAuthenticated)
			{

				var webBudgetExpeseQuery = new GetAllWebBudgetExpensesForLoggedusersQuery(userId!);
				var webBudgetExpense = await _mediator.Send(webBudgetExpeseQuery);

				var createExpenseView = new CreateExpenseView
				{
					Expenses = webBudgetExpense,
					ExpenseCommand = new ExpenseViewModelCommand
					{
						ExpenseCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!)
					}
				};


				return View(createExpenseView);
			}

			else
			{
				return RedirectToAction("NoAccess", "Home");
			}


		}
		// ------------------------------------------------- EDIT INCOME --------------------------------------------- //

		[HttpGet]
		public async Task<IActionResult> IncomeEdit(int incomeId)
		{
			var dto = await _mediator.Send(new GetWebBudgetIncomeByIDQuery(incomeId));

			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

			EditWebBudgetIncomeCommand model = _mapper.Map<EditWebBudgetIncomeCommand>(dto);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> IncomeEdit(int incomeId, EditWebBudgetIncomeCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(IncomesIndex2));


		}
		// ---------------------------------------- EDIT EXPENSE -------------------------------------------------- //


		[HttpGet]
		public async Task<IActionResult> ExpenseEdit(int expenseId)
		{
			var dto = await _mediator.Send(new GetWebBudgetExpenseByIDQuery(expenseId));

			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

			EditWebBudgetExpenseCommand model = _mapper.Map<EditWebBudgetExpenseCommand>(dto);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ExpenseEdit(int expenseId, EditWebBudgetExpenseCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(ExpensesIndex));
		}

		// ---------------------------------------- DELETE EXPENSE -------------------------------------------------- //

		[HttpPost]
		public async Task<IActionResult> ExpenseDelete(int expenseId)
		{
			var dto = await _mediator.Send(new GetWebBudgetExpenseByIDQuery(expenseId));

			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

			var command = new DeleteWebBudgetExpenseCommand
			{
				ExpenseId = expenseId
			};

			await _mediator.Send(command);


			return RedirectToAction(nameof(ExpensesIndex));
		}


		// ---------------------------------------- DELETE INCOME -------------------------------------------------- //

		[HttpPost]
		public async Task<IActionResult> DeleteIncome(int incomeId)
		{
			var dto = await _mediator.Send(new GetWebBudgetIncomeByIDQuery(incomeId));
			if (!dto.HasUserAccess)
			{
				return RedirectToAction("NoAccess", "Home");
			}

			var command = new DeleteWebBudgetIncomeeCommand
			{
				IncomeId = incomeId
			};

			await _mediator.Send(command);

			return RedirectToAction(nameof(IncomesIndex2));
		}

		// ---------------------------------------- CALCULATE BALANCE -------------------------------------------------- //
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> CalculateBalance(DateTime startDate, DateTime endDate)
		{
			var userId = _userManager.GetUserId(User);


			var incomes = await _webBudgetRepository.GetIncomesData(startDate, endDate, userId!);
			var expenses = await _webBudgetRepository.GetExpensesData(startDate, endDate, userId!);

			float totalIncomes = (float)Math.Round((double)incomes.Sum(i => i.IncomeValue), 2);
			float totalExpenses = (float)Math.Round((double)expenses.Sum(e => e.ExpenseValue), 2);

			float balance = totalIncomes - totalExpenses;

			float incomePercentage = (totalIncomes / (totalIncomes + totalExpenses)) * 100;
			float expensePercentage = (totalExpenses / (totalIncomes + totalExpenses)) * 100;

			var incomeNames = incomes.Select(i => i.IncomeType).ToList();
			var expenseNames = expenses.Select(e => e.ExpenseType).ToList();

			var incomeChartData = incomes.Select(item => new ChartData
			{
				Label = item.IncomeType,
				Value = item.IncomeValue,

			}).ToList();

			var expenseChartData = expenses.Select(item => new ChartData
			{
				Label = item.ExpenseType,
				Value = item.ExpenseValue,
			}).ToList();

			var balanceModel = new BalanceModel
			{
				Incomes = incomes,
				Expenses = expenses,
				TotalIncome = totalIncomes,
				TotalExpense = totalExpenses,
				Balance = balance,

				IncomeChartData = incomeChartData,
				ExpenseChartData = expenseChartData,

				IncomeName = incomeNames,
				ExpenseName = expenseNames

			};
			ViewBag.IncomePercentage = incomePercentage;
			ViewBag.ExpensePercentage = expensePercentage;

			return View(balanceModel);
		}




		// ---------------------------------------- ADD INCOME CATEGORY -------------------------------------------------- //

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddIncomeCategory(CreateIncomeCategoryCommand command)
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);

			var categoryName = command.CategoryName;

			var existingCategory = incomeCategories.FirstOrDefault(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

			if (existingCategory != null)
			{
				ModelState.AddModelError("CreateIncomeCategoryCommand.CategoryName", "Category with this name already exists.");
				var viewModel = new IncomeCategoryViewModel
				{
					CreateIncomeCategoryCommand = command,
					IncomeCategories = incomeCategories
				};
				return View("ShowIncomeCategories3", viewModel);
			}

			if (ModelState.IsValid)
			{
				await _mediator.Send(command);
			}

			return RedirectToAction(nameof(ShowIncomeCategories3));
		}
		public IActionResult AddIncomeCategory()
		{
			return View();
		}

		// ---------------------------------------- SHOW INCOME CATEGORY -------------------------------------------------- //

		[Authorize]
		public async Task<IActionResult> ShowIncomeCategories3()
		{
			var userId = _userManager.GetUserId(User);
			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);
			var newCategoryCommand = new CreateIncomeCategoryCommand();

			var viewModel = new IncomeCategoryViewModel
			{
				IncomeCategories = incomeCategories,
				CreateIncomeCategoryCommand = newCategoryCommand
			};

			return View(viewModel);
		}




		// ---------------------------------------- SHOW EXPENSE CATEGORY -------------------------------------------------- //

		[Authorize]
		public async Task<IActionResult> ShowExpenseCategories1()
		{
			var userId = _userManager.GetUserId(User);
			var expenseCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!);
			var newCategoryCommand = new CreateExpenseCategoryCommand();

			var viewModel = new ExpenseCategoryViewModel
			{
				ExpenseCategories = expenseCategories,
				ExpenseCommand = newCategoryCommand
			};

			return View(viewModel);

		}

		// ---------------------------------------- ADD EXPENSE CATEGORY -------------------------------------------------- //

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

			return RedirectToAction(nameof(ShowExpenseCategories1));
		}

		public IActionResult AddExpenseCategory()
		{
			return View();
		}

		// ---------------------------------------- MANAGEMENT -------------------------------------------------- //

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
			string loggedUserId = _userManager.GetUserId(User)!;

			await _webBudgetRepository.DeleteIncomeCategoryAndRelatedIncomesAsync(categoryId, loggedUserId);

			return RedirectToAction(nameof(ShowIncomeCategories3));
		}
		// ---------------------------------------- DELETE EXPENSE CATEGORY -------------------------------------------------- //


		[HttpPost]
		public async Task<IActionResult> DeleteExpenseCategory(int categoryId)
		{
			await _webBudgetRepository.DeleteExpenseCategoryAndRelateExpensesAsync(categoryId);

			return RedirectToAction(nameof(ShowExpenseCategories1));
		}

		// ---------------------------------------- EDIT INCOME CATEGORY -------------------------------------------------- //

		[HttpPost]
		public async Task<IActionResult> EditIncomeCategory(int categoryIdToEdit, string newCategoryName)
		{
			var userId = _userManager.GetUserId(User);

			var incomeCategories = await _webBudgetRepository.GetAllIncomeCategoriesForUser(userId!);

			var existingCategory = incomeCategories.FirstOrDefault(e => e.CategoryName.Equals(newCategoryName, StringComparison.OrdinalIgnoreCase));
			if (existingCategory != null)
			{
				ModelState.AddModelError("newCategoryName", "Category with the same name already exists.");
				ViewBag.CategoryExistsError = true;
				return RedirectToAction(nameof(ShowIncomeCategories3));
			}
			if (ModelState.IsValid)
			{
				await _webBudgetRepository.EditIncomeCategoryAsync(categoryIdToEdit, newCategoryName);

				await _webBudgetRepository.UpdateIncomeCategoryInIncomes(categoryIdToEdit, newCategoryName);

				return RedirectToAction(nameof(ShowIncomeCategories3));
			}

			ViewBag.CategoryName = newCategoryName;
			return RedirectToAction(nameof(ShowIncomeCategories3));
		}

		// ---------------------------------------- EDIT EXPENSE CATEGORY -------------------------------------------------- //

		[HttpPost]
		public async Task<IActionResult> EditExpenseCategory(int categoryIdToEdit, string newCategoryName)
		{
			var userId = _userManager.GetUserId(User);

			var expenseCategories = await _webBudgetRepository.GetAllExpenseCategoriesForUser(userId!);

			var existingCategory = expenseCategories.FirstOrDefault(e => e.CategoryName.Equals(newCategoryName, StringComparison.OrdinalIgnoreCase));
			if (existingCategory != null)
			{
				ModelState.AddModelError("newCategoryName", "Category with the same name already exists.");
				ViewBag.CategoryExistsError = true;
				return RedirectToAction(nameof(ShowExpenseCategories1));
			}
			if (ModelState.IsValid)
			{
				await _webBudgetRepository.EditExpenseCategoryAsync(categoryIdToEdit, newCategoryName);

				await _webBudgetRepository.UpdateExpenseCategoryInExpenses(categoryIdToEdit, newCategoryName);

				return RedirectToAction(nameof(ShowExpenseCategories1));
			}

			ViewBag.CategoryName = newCategoryName;
			return RedirectToAction(nameof(ShowExpenseCategories1));
		}

		// ----------------------------------------  GENEREATE PDF / CSV  -------------------------------------------------- //

		[HttpPost]
		public IActionResult GeneratePDF(BalanceModel model)
		{
			var balanceModel = new BalanceModel();


			byte[] pdfBytes = balanceModel.GeneratePdf(model);

			if (pdfBytes != null)
			{
				return File(pdfBytes, "application/pdf", "Budget.pdf");
			}
			else
			{
				return RedirectToAction("Error");
			}
		}

		[HttpPost]
		public IActionResult GenerateCSV(BalanceModel model)
		{
			string csvData = model.GenerateCsv(model);

			byte[] csvBytes = Encoding.UTF8.GetBytes(csvData);

			if (csvBytes != null)
			{
				return File(csvBytes, "text/csv", "Budget.csv");
			}
			else
			{
				return RedirectToAction("Error");
			}

		}

		// ----------------------------------------  DELETE -------------------------------------------------- //



	}
}

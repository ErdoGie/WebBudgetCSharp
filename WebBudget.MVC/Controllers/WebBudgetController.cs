using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBudget.Application.WebBudget.Commands.CategoryViewModels;
using WebBudget.Application.WebBudget.Commands.CreateExpenseCategory;
using WebBudget.Application.WebBudget.Commands.CreateExpenseViewModel;
using WebBudget.Application.WebBudget.Commands.CreateIncomeCategory;
using WebBudget.Application.WebBudget.Commands.CreateIncomeViewModel;
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

        [Route("WebBudget/Income/{IncomeId}/Edit")]
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
        [Route("WebBudget/Income/{IncomeId}/Edit")]
        public async Task<IActionResult> IncomeEdit(int incomeId, EditWebBudgetIncomeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            TempData["IncomeAdded"] = true;

            return RedirectToAction(nameof(IncomesIndex2));
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

        [Route("WebBudget/Income/{IncomeId}/Delete")]
        public async Task<IActionResult> IncomeDelete(int incomeId)
        {
            var dto = await _mediator.Send(new GetWebBudgetIncomeByIDQuery(incomeId));
            if (!dto.HasUserAccess)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            DeleteWebBudgetIncomeeCommand model = _mapper.Map<DeleteWebBudgetIncomeeCommand>(dto);


            return View(model);
        }

        [HttpPost]
        [Route("WebBudget/Income/{IncomeId}/Delete")]
        public async Task<IActionResult> IncomeDelete(int incomeId, DeleteWebBudgetIncomeeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            TempData["IncomeAdded"] = true;

            return RedirectToAction(nameof(IncomesIndex2));
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

/*			var categoryViewModels = new List<IncomeCategoryViewModel> { viewModel };
*/
			return View(viewModel);
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

            return RedirectToAction(nameof(ShowIncomeCategories3));
        }
        // ---------------------------------------- DELETE EXPENSE CATEGORY -------------------------------------------------- //


        [HttpPost]
        public async Task<IActionResult> DeleteExpenseCategory(int categoryId)
        {
            await _webBudgetRepository.DeleteExpenseCategoryAndRelateExpensesAsync(categoryId);

            return RedirectToAction(nameof(ManageExpense));
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





	}
}

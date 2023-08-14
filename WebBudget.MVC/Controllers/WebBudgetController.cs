using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using WebBudget.Application.WebBudget;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetExpense;
using WebBudget.Application.WebBudget.Commands.CreateWebBudgetIncome;
using WebBudget.Application.WebBudget.Commands.Queries.EditWebBudgets;
using WebBudget.Application.WebBudget.Commands.Queries.GetAllWebBudgetExpenses;
using WebBudget.Application.WebBudget.Commands.Queries.GetAllWEbBudgetIncomes;
using WebBudget.Application.WebBudget.Commands.Queries.GetWebBudgetIncomeByEncodedName;
using WebBudget.Domain.Entities;
using X.PagedList;
using static Azure.Core.HttpHeader;
//
namespace WebBudget.MVC.Controllers
{
	public class WebBudgetController : Controller
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		//przekazuje zależność 
		public WebBudgetController(IMediator mediator, IMapper mapper)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		// do tej metody przyjmuję dany typ budzetu
		[HttpPost]
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

		[HttpPost]
		public async Task<IActionResult> CreateExpense(CreateWebBudgetExpenseCommand command)
		{
			if (!ModelState.IsValid)
			{
				return View(command);
			}

			await _mediator.Send(command);

			return RedirectToAction(nameof(ExpensesIndex));
		}
		public async Task<IActionResult> IncomesIndex(int? page)
		{
			int pageSize = 6;
			int pageNumber = page ?? 1;

			var webBudgetIncome = await _mediator.Send(new GetAllWebBudgetIncomesQuery());

			var paginatedIncomeData = webBudgetIncome.ToPagedList(pageNumber, pageSize);

			int pageCount = (int)Math.Ceiling((double)webBudgetIncome.Count() / pageSize);

			ViewBag.PageCount = pageCount;

			return View(paginatedIncomeData);
		}

		public async Task<IActionResult> ExpensesIndex(int? page)
		{
			int pageSize = 6;
			int pageNumber = page ?? 1;
			var webBudgetExpese = await _mediator.Send(new GetAllWebBudgetExpensesQuery());

			var paginatedExpenseData = webBudgetExpese.ToPagedList(pageNumber, pageSize);

			int pageCount = (int)Math.Ceiling((double)webBudgetExpese.Count() / pageSize);

			ViewBag.PageCount = pageCount;

			return View(paginatedExpenseData);
		}

		[Route("WebBudget/Income/{encodedIncomeName}/Edit")]
		public async Task<IActionResult> IncomeEdit(string encodedIncomeName)
		{
			var dto = await _mediator.Send(new GetWebBudgetIncomeByEncodedNameQuery(encodedIncomeName));

			EditWebBudgetIncomeCommand model = _mapper.Map<EditWebBudgetIncomeCommand>(dto);

			
			model.EncodedIncomeName = model.IncomeType.ToLower().Replace(" ", "-");
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

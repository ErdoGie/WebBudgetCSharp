﻿using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.Balance
{
    public class BalanceModel
    {
        public IEnumerable<Domain.Entities.WebBudgetIncome>? Incomes { get; set; }

        public IEnumerable<Domain.Entities.WebBudgetExpense>? Expenses { get; set; }

        public float TotalIncome { get; set; }

        public float TotalExpense { get; set; }

        public float Balance { get; set; }


        public List<ChartData>? IncomeChartData { get; set; }
        public List<ChartData>? ExpenseChartData { get; set; }

        public List<string>? IncomeName { get; set; }

        public List<string>? ExpenseName { get; set; }

        public List<float>? IncomeValue {get;set;}
        public List<DateTime>? IncomeDate { get; set; }


		public byte[] GeneratePdf(BalanceModel model)
		{
			Document document = new Document();
			MemoryStream memoryStream = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
			document.Open();

			PdfPTable incomeTable = new PdfPTable(3);
			incomeTable.AddCell("Category");
			incomeTable.AddCell("Value");
			incomeTable.AddCell("Date");

			for (int i = 0; i < model.IncomeName!.Count; i++)
			{
				incomeTable.AddCell(model.IncomeName[i]);
				incomeTable.AddCell(model.IncomeValue![i].ToString("F2") + " zl");
				incomeTable.AddCell(model.IncomeDate![i].ToString());
			}

			document.Add(incomeTable);

			document.Add(new Paragraph(" "));


			PdfPTable expenseTable = new PdfPTable(2);
			expenseTable.AddCell("Expense Name");
			expenseTable.AddCell("Expense Value");


			document.Add(expenseTable);

			document.Add(new Paragraph("Total Income: " + model.TotalIncome.ToString("F2") + " zl"));
			document.Add(new Paragraph("Total Expense: " + model.TotalExpense.ToString("F2") + " zl"));
			document.Add(new Paragraph("Balance: " + model.Balance.ToString("F2") + " zl"));

			document.Close();

			byte[] pdfBytes = memoryStream.ToArray();
			return pdfBytes;
		}


	}
}

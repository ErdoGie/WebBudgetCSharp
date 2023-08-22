using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseKolumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetExpense_IncomeCategories_CategoryId",
                table: "WebBudgetExpense");

            migrationBuilder.DropIndex(
                name: "IX_WebBudgetExpense_CategoryId",
                table: "WebBudgetExpense");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "WebBudgetExpense",
                newName: "ExpenseCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseCategoryId",
                table: "WebBudgetExpense",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WebBudgetExpense_CategoryId",
                table: "WebBudgetExpense",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetExpense_IncomeCategories_CategoryId",
                table: "WebBudgetExpense",
                column: "CategoryId",
                principalTable: "IncomeCategories",
                principalColumn: "CategoryId");
        }
    }
}

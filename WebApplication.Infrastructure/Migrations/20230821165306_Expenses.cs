using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Expenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "WebBudgetExpense",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetExpense_IncomeCategories_CategoryId",
                table: "WebBudgetExpense");

            migrationBuilder.DropIndex(
                name: "IX_WebBudgetExpense_CategoryId",
                table: "WebBudgetExpense");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "WebBudgetExpense");
        }
    }
}

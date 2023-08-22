using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategories_CategoryId",
                table: "WebBudgetIncome");

            migrationBuilder.DropIndex(
                name: "IX_WebBudgetIncome_CategoryId",
                table: "WebBudgetIncome");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "WebBudgetIncome",
                newName: "IncomeCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncomeCategoryId",
                table: "WebBudgetIncome",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WebBudgetIncome_CategoryId",
                table: "WebBudgetIncome",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategories_CategoryId",
                table: "WebBudgetIncome",
                column: "CategoryId",
                principalTable: "IncomeCategories",
                principalColumn: "CategoryId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeCategory_AspNetUsers_UserId",
                table: "IncomeCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategory_CategoryId",
                table: "WebBudgetIncome");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeCategory",
                table: "IncomeCategory");

            migrationBuilder.RenameTable(
                name: "IncomeCategory",
                newName: "IncomeCategories");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeCategory_UserId",
                table: "IncomeCategories",
                newName: "IX_IncomeCategories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeCategories",
                table: "IncomeCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeCategories_AspNetUsers_UserId",
                table: "IncomeCategories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategories_CategoryId",
                table: "WebBudgetIncome",
                column: "CategoryId",
                principalTable: "IncomeCategories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeCategories_AspNetUsers_UserId",
                table: "IncomeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategories_CategoryId",
                table: "WebBudgetIncome");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeCategories",
                table: "IncomeCategories");

            migrationBuilder.RenameTable(
                name: "IncomeCategories",
                newName: "IncomeCategory");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeCategories_UserId",
                table: "IncomeCategory",
                newName: "IX_IncomeCategory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeCategory",
                table: "IncomeCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeCategory_AspNetUsers_UserId",
                table: "IncomeCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategory_CategoryId",
                table: "WebBudgetIncome",
                column: "CategoryId",
                principalTable: "IncomeCategory",
                principalColumn: "CategoryId");
        }
    }
}

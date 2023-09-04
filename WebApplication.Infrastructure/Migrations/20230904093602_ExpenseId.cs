using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncodedExpenseName",
                table: "WebBudgetExpense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncodedExpenseName",
                table: "WebBudgetExpense",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

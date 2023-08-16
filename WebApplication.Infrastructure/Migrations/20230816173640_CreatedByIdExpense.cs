using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "WebBudgetExpense",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebBudgetExpense_CreatedById",
                table: "WebBudgetExpense",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetExpense_AspNetUsers_CreatedById",
                table: "WebBudgetExpense",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetExpense_AspNetUsers_CreatedById",
                table: "WebBudgetExpense");

            migrationBuilder.DropIndex(
                name: "IX_WebBudgetExpense_CreatedById",
                table: "WebBudgetExpense");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "WebBudgetExpense");
        }
    }
}

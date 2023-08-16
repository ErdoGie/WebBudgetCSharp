using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "WebBudgetIncome",
                type: "nvarchar(450)",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_WebBudgetIncome_CreatedById",
                table: "WebBudgetIncome",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetIncome_AspNetUsers_CreatedById",
                table: "WebBudgetIncome",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetIncome_AspNetUsers_CreatedById",
                table: "WebBudgetIncome");

            migrationBuilder.DropIndex(
                name: "IX_WebBudgetIncome_CreatedById",
                table: "WebBudgetIncome");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "WebBudgetIncome");

        
        }
    }
}

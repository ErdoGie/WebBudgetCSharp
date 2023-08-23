using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIncome1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "WebBudgetIncome",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IncomeCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategory", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_IncomeCategory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebBudgetIncome_CategoryId",
                table: "WebBudgetIncome",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategory_UserId",
                table: "IncomeCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategory_CategoryId",
                table: "WebBudgetIncome",
                column: "CategoryId",
                principalTable: "IncomeCategory",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebBudgetIncome_IncomeCategory_CategoryId",
                table: "WebBudgetIncome");

            migrationBuilder.DropTable(
                name: "IncomeCategory");

            migrationBuilder.DropIndex(
                name: "IX_WebBudgetIncome_CategoryId",
                table: "WebBudgetIncome");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "WebBudgetIncome");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncodedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebBudgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetExpense",
                columns: table => new
                {
                    WebBudgetId = table.Column<int>(type: "int", nullable: false),
                    ExpenseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetExpense", x => x.WebBudgetId);
                    table.ForeignKey(
                        name: "FK_BudgetExpense_WebBudgets_WebBudgetId",
                        column: x => x.WebBudgetId,
                        principalTable: "WebBudgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetIncome",
                columns: table => new
                {
                    WebBudgetId = table.Column<int>(type: "int", nullable: false),
                    IncomeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncomeValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetIncome", x => x.WebBudgetId);
                    table.ForeignKey(
                        name: "FK_BudgetIncome_WebBudgets_WebBudgetId",
                        column: x => x.WebBudgetId,
                        principalTable: "WebBudgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetExpense");

            migrationBuilder.DropTable(
                name: "BudgetIncome");

            migrationBuilder.DropTable(
                name: "WebBudgets");
        }
    }
}

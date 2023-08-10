﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebBudget.Infrastructure.Persistance;

#nullable disable

namespace WebBudget.Infrastructure.Migrations
{
    [DbContext(typeof(WebBudgetDbContext))]
    [Migration("20230809113126_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebBudget.Domain.Entities.WebBudget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("WebBudgets");
                });

            modelBuilder.Entity("WebBudget.Domain.Entities.WebBudget", b =>
                {
                    b.OwnsOne("WebBudget.Domain.Entities.WebBudgetExpense", "BudgetExpense", b1 =>
                        {
                            b1.Property<int>("WebBudgetId")
                                .HasColumnType("int");

                            b1.Property<string>("EncodedExpenseName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("ExpenseDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ExpenseType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<float>("ExpenseValue")
                                .HasColumnType("real");

                            b1.HasKey("WebBudgetId");

                            b1.ToTable("BudgetExpense", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WebBudgetId");
                        });

                    b.OwnsOne("WebBudget.Domain.Entities.WebBudgetIncome", "BudgetIncome", b1 =>
                        {
                            b1.Property<int>("WebBudgetId")
                                .HasColumnType("int");

                            b1.Property<string>("EncodedIncomeName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("IncomeDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("IncomeType")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<float>("IncomeValue")
                                .HasColumnType("real");

                            b1.HasKey("WebBudgetId");

                            b1.ToTable("BudgetIncome", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WebBudgetId");
                        });

                    b.Navigation("BudgetExpense")
                        .IsRequired();

                    b.Navigation("BudgetIncome")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

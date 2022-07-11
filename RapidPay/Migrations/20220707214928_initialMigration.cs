using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RapidPay.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creditCards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardId = table.Column<long>(type: "bigint", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentFees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lastFee = table.Column<double>(type: "float", nullable: false),
                    currentFee = table.Column<double>(type: "float", nullable: false),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentFees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paymentsLogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cardId = table.Column<long>(type: "bigint", nullable: false),
                    fee = table.Column<double>(type: "float", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    cardBalence = table.Column<double>(type: "float", nullable: false),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentsLogs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creditCards");

            migrationBuilder.DropTable(
                name: "paymentFees");

            migrationBuilder.DropTable(
                name: "paymentsLogs");
        }
    }
}

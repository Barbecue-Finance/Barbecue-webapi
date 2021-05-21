using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class OperationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "OutComeMoneyOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "IncomeMoneyOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OutComeMoneyOperations_UserId",
                table: "OutComeMoneyOperations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeMoneyOperations_UserId",
                table: "IncomeMoneyOperations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeMoneyOperations_UserAccounts_UserId",
                table: "IncomeMoneyOperations",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutComeMoneyOperations_UserAccounts_UserId",
                table: "OutComeMoneyOperations",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeMoneyOperations_UserAccounts_UserId",
                table: "IncomeMoneyOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_OutComeMoneyOperations_UserAccounts_UserId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropIndex(
                name: "IX_OutComeMoneyOperations_UserId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropIndex(
                name: "IX_IncomeMoneyOperations_UserId",
                table: "IncomeMoneyOperations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IncomeMoneyOperations");
        }
    }
}

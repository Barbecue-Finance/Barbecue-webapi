using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class OperationCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperationCategoryId",
                table: "OutComeMoneyOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OperationCategoryId",
                table: "IncomeMoneyOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "OperationCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PurseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationCategory_Purses_PurseId",
                        column: x => x.PurseId,
                        principalTable: "Purses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutComeMoneyOperations_OperationCategoryId",
                table: "OutComeMoneyOperations",
                column: "OperationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeMoneyOperations_OperationCategoryId",
                table: "IncomeMoneyOperations",
                column: "OperationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationCategory_PurseId",
                table: "OperationCategory",
                column: "PurseId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "IncomeMoneyOperations",
                column: "OperationCategoryId",
                principalTable: "OperationCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutComeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "OutComeMoneyOperations",
                column: "OperationCategoryId",
                principalTable: "OperationCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "IncomeMoneyOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_OutComeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropTable(
                name: "OperationCategory");

            migrationBuilder.DropIndex(
                name: "IX_OutComeMoneyOperations_OperationCategoryId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropIndex(
                name: "IX_IncomeMoneyOperations_OperationCategoryId",
                table: "IncomeMoneyOperations");

            migrationBuilder.DropColumn(
                name: "OperationCategoryId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropColumn(
                name: "OperationCategoryId",
                table: "IncomeMoneyOperations");
        }
    }
}

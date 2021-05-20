using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class AgainOperationCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "IncomeMoneyOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationCategory_Purses_PurseId",
                table: "OperationCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_OutComeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationCategory",
                table: "OperationCategory");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "OperationCategory");

            migrationBuilder.RenameTable(
                name: "OperationCategory",
                newName: "OutComeOperationCategories");

            migrationBuilder.RenameColumn(
                name: "OperationCategoryId",
                table: "OutComeMoneyOperations",
                newName: "OutComeOperationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OutComeMoneyOperations_OperationCategoryId",
                table: "OutComeMoneyOperations",
                newName: "IX_OutComeMoneyOperations_OutComeOperationCategoryId");

            migrationBuilder.RenameColumn(
                name: "OperationCategoryId",
                table: "IncomeMoneyOperations",
                newName: "IncomeOperationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeMoneyOperations_OperationCategoryId",
                table: "IncomeMoneyOperations",
                newName: "IX_IncomeMoneyOperations_IncomeOperationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OperationCategory_PurseId",
                table: "OutComeOperationCategories",
                newName: "IX_OutComeOperationCategories_PurseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutComeOperationCategories",
                table: "OutComeOperationCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IncomeOperationCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PurseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeOperationCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeOperationCategories_Purses_PurseId",
                        column: x => x.PurseId,
                        principalTable: "Purses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomeOperationCategories_PurseId",
                table: "IncomeOperationCategories",
                column: "PurseId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeMoneyOperations_IncomeOperationCategories_IncomeOpera~",
                table: "IncomeMoneyOperations",
                column: "IncomeOperationCategoryId",
                principalTable: "IncomeOperationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutComeMoneyOperations_OutComeOperationCategories_OutComeOp~",
                table: "OutComeMoneyOperations",
                column: "OutComeOperationCategoryId",
                principalTable: "OutComeOperationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutComeOperationCategories_Purses_PurseId",
                table: "OutComeOperationCategories",
                column: "PurseId",
                principalTable: "Purses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeMoneyOperations_IncomeOperationCategories_IncomeOpera~",
                table: "IncomeMoneyOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_OutComeMoneyOperations_OutComeOperationCategories_OutComeOp~",
                table: "OutComeMoneyOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_OutComeOperationCategories_Purses_PurseId",
                table: "OutComeOperationCategories");

            migrationBuilder.DropTable(
                name: "IncomeOperationCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OutComeOperationCategories",
                table: "OutComeOperationCategories");

            migrationBuilder.RenameTable(
                name: "OutComeOperationCategories",
                newName: "OperationCategory");

            migrationBuilder.RenameColumn(
                name: "OutComeOperationCategoryId",
                table: "OutComeMoneyOperations",
                newName: "OperationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OutComeMoneyOperations_OutComeOperationCategoryId",
                table: "OutComeMoneyOperations",
                newName: "IX_OutComeMoneyOperations_OperationCategoryId");

            migrationBuilder.RenameColumn(
                name: "IncomeOperationCategoryId",
                table: "IncomeMoneyOperations",
                newName: "OperationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeMoneyOperations_IncomeOperationCategoryId",
                table: "IncomeMoneyOperations",
                newName: "IX_IncomeMoneyOperations_OperationCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OutComeOperationCategories_PurseId",
                table: "OperationCategory",
                newName: "IX_OperationCategory_PurseId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "OperationCategory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationCategory",
                table: "OperationCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeMoneyOperations_OperationCategory_OperationCategoryId",
                table: "IncomeMoneyOperations",
                column: "OperationCategoryId",
                principalTable: "OperationCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationCategory_Purses_PurseId",
                table: "OperationCategory",
                column: "PurseId",
                principalTable: "Purses",
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
    }
}

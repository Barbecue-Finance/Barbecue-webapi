using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class RenameGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purses_Groups_UserGroupId",
                table: "Purses");

            migrationBuilder.RenameColumn(
                name: "UserGroupId",
                table: "Purses",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Purses_UserGroupId",
                table: "Purses",
                newName: "IX_Purses_GroupId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedAt",
                table: "Invites",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Purses_Groups_GroupId",
                table: "Purses",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purses_Groups_GroupId",
                table: "Purses");

            migrationBuilder.DropColumn(
                name: "ResolvedAt",
                table: "Invites");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Purses",
                newName: "UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Purses_GroupId",
                table: "Purses",
                newName: "IX_Purses_UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purses_Groups_UserGroupId",
                table: "Purses",
                column: "UserGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

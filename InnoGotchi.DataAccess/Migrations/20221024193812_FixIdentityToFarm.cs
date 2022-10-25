using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class FixIdentityToFarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Farms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Collaborators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Farms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Collaborators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id");
        }
    }
}

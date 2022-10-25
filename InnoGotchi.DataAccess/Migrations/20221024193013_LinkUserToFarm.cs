using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class LinkUserToFarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Farms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farms_UserId",
                table: "Farms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_IdentityUsers_UserId",
                table: "Collaborators",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_IdentityUsers_UserId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms");

            migrationBuilder.DropIndex(
                name: "IX_Farms_UserId",
                table: "Farms");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Farms");
        }
    }
}

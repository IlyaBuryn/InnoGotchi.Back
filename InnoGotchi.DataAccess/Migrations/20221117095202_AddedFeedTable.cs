using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class AddedFeedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<int>(type: "int", nullable: false),
                    FeedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    FoodCount = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    WaterCount = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedsInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedsInfo_IdentityUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedsInfo_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedsInfo_IdentityUserId",
                table: "FeedsInfo",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedsInfo_PetId",
                table: "FeedsInfo",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedsInfo");
        }
    }
}

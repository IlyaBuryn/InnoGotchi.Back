using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyPartTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyPartTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyPartTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyParts_BodyPartTypes_BodyPartTypeId",
                        column: x => x.BodyPartTypeId,
                        principalTable: "BodyPartTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborators_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    BodyId = table.Column<int>(type: "int", nullable: true),
                    EyeId = table.Column<int>(type: "int", nullable: true),
                    NoseId = table.Column<int>(type: "int", nullable: true),
                    MouthId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_BodyParts_BodyId",
                        column: x => x.BodyId,
                        principalTable: "BodyParts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_BodyParts_EyeId",
                        column: x => x.EyeId,
                        principalTable: "BodyParts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_BodyParts_MouthId",
                        column: x => x.MouthId,
                        principalTable: "BodyParts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_BodyParts_NoseId",
                        column: x => x.NoseId,
                        principalTable: "BodyParts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false),
                    HungerLevel = table.Column<int>(type: "int", nullable: false),
                    ThirsyLevel = table.Column<int>(type: "int", nullable: false),
                    HappinessDaysCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalSigns_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyParts_BodyPartTypeId",
                table: "BodyParts",
                column: "BodyPartTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_FarmId",
                table: "Collaborators",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BodyId",
                table: "Pets",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_EyeId",
                table: "Pets",
                column: "EyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_FarmId",
                table: "Pets",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_MouthId",
                table: "Pets",
                column: "MouthId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_NoseId",
                table: "Pets",
                column: "NoseId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PetId",
                table: "VitalSigns",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "VitalSigns");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "BodyParts");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "BodyPartTypes");
        }
    }
}

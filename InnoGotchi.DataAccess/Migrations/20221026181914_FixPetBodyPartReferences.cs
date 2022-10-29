using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class FixPetBodyPartReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_BodyParts_BodyId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_BodyParts_EyeId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_BodyParts_MouthId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_BodyParts_NoseId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_BodyId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_EyeId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_MouthId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_NoseId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "BodyId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "EyeId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "MouthId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "NoseId",
                table: "Pets");

            migrationBuilder.CreateTable(
                name: "BodyPartPet",
                columns: table => new
                {
                    BodyPartsId = table.Column<int>(type: "int", nullable: false),
                    PetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyPartPet", x => new { x.BodyPartsId, x.PetsId });
                    table.ForeignKey(
                        name: "FK_BodyPartPet_BodyParts_BodyPartsId",
                        column: x => x.BodyPartsId,
                        principalTable: "BodyParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyPartPet_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyPartPet_PetsId",
                table: "BodyPartPet",
                column: "PetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyPartPet");

            migrationBuilder.AddColumn<int>(
                name: "BodyId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EyeId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MouthId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoseId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BodyId",
                table: "Pets",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_EyeId",
                table: "Pets",
                column: "EyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_MouthId",
                table: "Pets",
                column: "MouthId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_NoseId",
                table: "Pets",
                column: "NoseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_BodyParts_BodyId",
                table: "Pets",
                column: "BodyId",
                principalTable: "BodyParts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_BodyParts_EyeId",
                table: "Pets",
                column: "EyeId",
                principalTable: "BodyParts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_BodyParts_MouthId",
                table: "Pets",
                column: "MouthId",
                principalTable: "BodyParts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_BodyParts_NoseId",
                table: "Pets",
                column: "NoseId",
                principalTable: "BodyParts",
                principalColumn: "Id");
        }
    }
}

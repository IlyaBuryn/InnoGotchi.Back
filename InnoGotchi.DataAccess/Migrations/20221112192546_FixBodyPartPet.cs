using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class FixBodyPartPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyPartPet_BodyParts_BodyPartsId",
                table: "BodyPartPet");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyPartPet_Pets_PetsId",
                table: "BodyPartPet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyPartPet",
                table: "BodyPartPet");

            migrationBuilder.RenameTable(
                name: "BodyPartPet",
                newName: "BodyPartPets");

            migrationBuilder.RenameIndex(
                name: "IX_BodyPartPet_PetsId",
                table: "BodyPartPets",
                newName: "IX_BodyPartPets_PetsId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BodyPartPets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyPartPets",
                table: "BodyPartPets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BodyPartPets_BodyPartsId",
                table: "BodyPartPets",
                column: "BodyPartsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyPartPets_BodyParts_BodyPartsId",
                table: "BodyPartPets",
                column: "BodyPartsId",
                principalTable: "BodyParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyPartPets_Pets_PetsId",
                table: "BodyPartPets",
                column: "PetsId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyPartPets_BodyParts_BodyPartsId",
                table: "BodyPartPets");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyPartPets_Pets_PetsId",
                table: "BodyPartPets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyPartPets",
                table: "BodyPartPets");

            migrationBuilder.DropIndex(
                name: "IX_BodyPartPets_BodyPartsId",
                table: "BodyPartPets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BodyPartPets");

            migrationBuilder.RenameTable(
                name: "BodyPartPets",
                newName: "BodyPartPet");

            migrationBuilder.RenameIndex(
                name: "IX_BodyPartPets_PetsId",
                table: "BodyPartPet",
                newName: "IX_BodyPartPet_PetsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyPartPet",
                table: "BodyPartPet",
                columns: new[] { "BodyPartsId", "PetsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BodyPartPet_BodyParts_BodyPartsId",
                table: "BodyPartPet",
                column: "BodyPartsId",
                principalTable: "BodyParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyPartPet_Pets_PetsId",
                table: "BodyPartPet",
                column: "PetsId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "~/images/noses/nose_1.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "~/images/mouths/mouth_1.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "~/images/noses/nose_2.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 7,
                column: "Image",
                value: "~/images/mouths/mouth_2.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "~/images/noses/nose_3.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "~/images/mouths/mouth_3.svg");

            migrationBuilder.InsertData(
                table: "BodyParts",
                columns: new[] { "Id", "BodyPartTypeId", "Color", "Image" },
                values: new object[,]
                {
                    { 13, 1, null, "~/images/bodies/body_4.svg" },
                    { 14, 2, null, "~/images/noses/nose_4.svg" },
                    { 15, 3, null, "~/images/mouths/mouth_4.svg" },
                    { 16, 4, null, "~/images/eyes/eye_4.svg" },
                    { 17, 1, null, "~/images/bodies/body_5.svg" },
                    { 18, 2, null, "~/images/noses/nose_5.svg" },
                    { 19, 3, null, "~/images/mouths/mouth_5.svg" },
                    { 20, 4, null, "~/images/eyes/eye_5.svg" },
                    { 21, 2, null, "~/images/noses/nose_6.svg" },
                    { 22, 4, null, "~/images/eyes/eye_6.svg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "~/images/bodies/nose_1.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "~/images/bodies/mouth_1.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "~/images/bodies/nose_2.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 7,
                column: "Image",
                value: "~/images/bodies/mouth_2.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "~/images/bodies/nose_3.svg");

            migrationBuilder.UpdateData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "~/images/bodies/mouth_3.svg");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.DataAccess.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyParts_BodyPartTypes_BodyPartTypeId",
                table: "BodyParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_IdentityUsers_UserId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Farms_IdentityUsers_UserId",
                table: "Farms");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_IdentityRoles_RoleId",
                table: "IdentityUsers");

            migrationBuilder.DropIndex(
                name: "IX_VitalSigns_PetId",
                table: "VitalSigns");

            migrationBuilder.DropIndex(
                name: "IX_Farms_UserId",
                table: "Farms");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "IdentityUsers",
                newName: "IdentityRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUsers_RoleId",
                table: "IdentityUsers",
                newName: "IX_IdentityUsers_IdentityRoleId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Farms",
                newName: "IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Collaborators",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                newName: "IX_Collaborators_IdentityUserId");

            migrationBuilder.AlterColumn<int>(
                name: "ThirsyLevel",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAlive",
                table: "VitalSigns",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "HungerLevel",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HappinessDaysCount",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "IdentityUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "IdentityUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IdentityRoles",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Farms",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Collaborators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "BodyParts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "BodyParts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BodyPartTypeId",
                table: "BodyParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "BodyPartTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Body" },
                    { 2, "Nose" },
                    { 3, "Mouth" },
                    { 4, "Eye" }
                });

            migrationBuilder.InsertData(
                table: "IdentityRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "BodyParts",
                columns: new[] { "Id", "BodyPartTypeId", "Color", "Image" },
                values: new object[,]
                {
                    { 1, 1, "#ffffff", "testbody_1.png" },
                    { 2, 2, "#ffffff", "testnose_1.png" },
                    { 3, 3, "#ffffff", "testmouth_1.png" },
                    { 4, 4, "#ffffff", "testeye_1.png" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "IdentityRoleId", "Image", "Name", "Password", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, 1, null, "Ilya", "admin", null, "admin" },
                    { 2, 2, null, "ConfName1", "123456", "ConfSurname1", "ConfUser1@m.com" },
                    { 3, 2, null, "ConfName2", "234567", "ConfSurname2", "ConfUser2@m.com" }
                });

            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "Id", "IdentityUserId", "Name" },
                values: new object[] { 1, 1, "ConfTestFarm_1" });

            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "Id", "IdentityUserId", "Name" },
                values: new object[] { 2, 2, "ConfTestFarm_2" });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "FarmId", "Name" },
                values: new object[] { 1, 1, "ConfTestName_1" });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "FarmId", "Name" },
                values: new object[] { 2, 1, "ConfTestName_2" });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "FarmId", "Name" },
                values: new object[] { 3, 2, "ConfTestName_3" });

            migrationBuilder.InsertData(
                table: "VitalSigns",
                columns: new[] { "Id", "PetId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "VitalSigns",
                columns: new[] { "Id", "HappinessDaysCount", "HungerLevel", "PetId", "ThirsyLevel" },
                values: new object[] { 2, 1, 1, 2, 2 });

            migrationBuilder.InsertData(
                table: "VitalSigns",
                columns: new[] { "Id", "HappinessDaysCount", "HungerLevel", "PetId", "ThirsyLevel" },
                values: new object[] { 3, 5, 2, 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PetId",
                table: "VitalSigns",
                column: "PetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_Name",
                table: "Pets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_Username",
                table: "IdentityUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farms_IdentityUserId",
                table: "Farms",
                column: "IdentityUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farms_Name",
                table: "Farms",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyParts_BodyPartTypes_BodyPartTypeId",
                table: "BodyParts",
                column: "BodyPartTypeId",
                principalTable: "BodyPartTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_IdentityUsers_IdentityUserId",
                table: "Collaborators",
                column: "IdentityUserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_IdentityUsers_IdentityUserId",
                table: "Farms",
                column: "IdentityUserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUsers_IdentityRoles_IdentityRoleId",
                table: "IdentityUsers",
                column: "IdentityRoleId",
                principalTable: "IdentityRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyParts_BodyPartTypes_BodyPartTypeId",
                table: "BodyParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_IdentityUsers_IdentityUserId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Farms_IdentityUsers_IdentityUserId",
                table: "Farms");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_IdentityRoles_IdentityRoleId",
                table: "IdentityUsers");

            migrationBuilder.DropIndex(
                name: "IX_VitalSigns_PetId",
                table: "VitalSigns");

            migrationBuilder.DropIndex(
                name: "IX_Pets_Name",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUsers_Username",
                table: "IdentityUsers");

            migrationBuilder.DropIndex(
                name: "IX_Farms_IdentityUserId",
                table: "Farms");

            migrationBuilder.DropIndex(
                name: "IX_Farms_Name",
                table: "Farms");

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BodyParts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VitalSigns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VitalSigns",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VitalSigns",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BodyPartTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BodyPartTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BodyPartTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BodyPartTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IdentityRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "IdentityRoleId",
                table: "IdentityUsers",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUsers_IdentityRoleId",
                table: "IdentityUsers",
                newName: "IX_IdentityUsers_RoleId");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Farms",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Collaborators",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_IdentityUserId",
                table: "Collaborators",
                newName: "IX_Collaborators_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ThirsyLevel",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAlive",
                table: "VitalSigns",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "HungerLevel",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HappinessDaysCount",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IdentityRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Farms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "Collaborators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "BodyParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "BodyParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BodyPartTypeId",
                table: "BodyParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PetId",
                table: "VitalSigns",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_UserId",
                table: "Farms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyParts_BodyPartTypes_BodyPartTypeId",
                table: "BodyParts",
                column: "BodyPartTypeId",
                principalTable: "BodyPartTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Farms_FarmId",
                table: "Collaborators",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUsers_IdentityRoles_RoleId",
                table: "IdentityUsers",
                column: "RoleId",
                principalTable: "IdentityRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

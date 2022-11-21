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
                name: "IdentityRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyPartTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyParts_BodyPartTypes_BodyPartTypeId",
                        column: x => x.BodyPartTypeId,
                        principalTable: "BodyPartTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_IdentityRoles_IdentityRoleId",
                        column: x => x.IdentityRoleId,
                        principalTable: "IdentityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IdentityUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farms_IdentityUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Collaborators_IdentityUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyPartPets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyPartsId = table.Column<int>(type: "int", nullable: false),
                    PetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyPartPets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyPartPets_BodyParts_BodyPartsId",
                        column: x => x.BodyPartsId,
                        principalTable: "BodyParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyPartPets_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<int>(type: "int", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    HungerLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ThirstyLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    HappinessDaysCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
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
                    { 1, 1, null, "~/images/bodies/body_1.svg" },
                    { 2, 2, null, "~/images/bodies/nose_1.svg" },
                    { 3, 3, null, "~/images/bodies/mouth_1.svg" },
                    { 4, 4, null, "~/images/eyes/eye_1.svg" },
                    { 5, 1, null, "~/images/bodies/body_2.svg" },
                    { 6, 2, null, "~/images/bodies/nose_2.svg" },
                    { 7, 3, null, "~/images/bodies/mouth_2.svg" },
                    { 8, 4, null, "~/images/eyes/eye_2.svg" },
                    { 9, 1, null, "~/images/bodies/body_3.svg" },
                    { 10, 2, null, "~/images/bodies/nose_3.svg" },
                    { 11, 3, null, "~/images/bodies/mouth_3.svg" },
                    { 12, 4, null, "~/images/eyes/eye_3.svg" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "IdentityRoleId", "Image", "Name", "Password", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, 1, null, "Ilya", "qweqwe", null, "admin@m.com" },
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
                table: "BodyPartPets",
                columns: new[] { "Id", "BodyPartsId", "PetsId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 2 },
                    { 6, 6, 2 },
                    { 7, 7, 2 },
                    { 8, 8, 2 },
                    { 9, 9, 3 },
                    { 10, 10, 3 },
                    { 11, 11, 3 },
                    { 12, 12, 3 }
                });

            migrationBuilder.InsertData(
                table: "VitalSigns",
                columns: new[] { "Id", "PetId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "VitalSigns",
                columns: new[] { "Id", "HappinessDaysCount", "HungerLevel", "PetId", "ThirstyLevel" },
                values: new object[,]
                {
                    { 2, 1, 1, 2, 2 },
                    { 3, 5, 2, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyPartPets_BodyPartsId",
                table: "BodyPartPets",
                column: "BodyPartsId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyPartPets_PetsId",
                table: "BodyPartPets",
                column: "PetsId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyParts_BodyPartTypeId",
                table: "BodyParts",
                column: "BodyPartTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_FarmId",
                table: "Collaborators",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_IdentityUserId",
                table: "Collaborators",
                column: "IdentityUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_FeedsInfo_IdentityUserId",
                table: "FeedsInfo",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedsInfo_PetId",
                table: "FeedsInfo",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_IdentityRoleId",
                table: "IdentityUsers",
                column: "IdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_Username",
                table: "IdentityUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_FarmId",
                table: "Pets",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_Name",
                table: "Pets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PetId",
                table: "VitalSigns",
                column: "PetId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyPartPets");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "FeedsInfo");

            migrationBuilder.DropTable(
                name: "VitalSigns");

            migrationBuilder.DropTable(
                name: "BodyParts");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "BodyPartTypes");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "IdentityUsers");

            migrationBuilder.DropTable(
                name: "IdentityRoles");
        }
    }
}

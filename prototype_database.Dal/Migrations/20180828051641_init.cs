using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace prototype_database.Dal.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942"), "Rosen" },
                    { new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda"), "UIT" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("fa83781c-c13e-4b2a-a13b-cc557cfba720"), "Technical Lead" },
                    { new Guid("77817bb6-2a22-4635-8dda-b820356ed8f9"), "HR Lead" },
                    { new Guid("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "GroupId", "IsMain", "UserId" },
                values: new object[,]
                {
                    { new Guid("540ab5fc-4615-4cde-a284-cc9d9698a238"), new Guid("3777ec35-2393-4053-95ad-cc587d87a3e3"), true, "12345" },
                    { new Guid("a2bc5162-d036-436e-9ac2-ab4571ec0694"), new Guid("ab2ace08-2daf-4422-9242-293025aab9f6"), false, "12345" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "IsMain", "RoleId", "UserId" },
                values: new object[] { new Guid("d0bd5e70-f6b3-4671-b587-0a87995daf84"), true, new Guid("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"), "12345" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "OrganizationId" },
                values: new object[,]
                {
                    { new Guid("3777ec35-2393-4053-95ad-cc587d87a3e3"), "Technical", new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942") },
                    { new Guid("ab2ace08-2daf-4422-9242-293025aab9f6"), "HR", new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942") },
                    { new Guid("abba6119-b935-4870-9c06-be6b8872fb32"), "SoftwareEngineer", new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda") },
                    { new Guid("f90317a4-a87c-4800-8d24-8e7c5e84073e"), "ComputerEngineer", new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Mobile", "OrganizationId", "Phone", "ProfileImage" },
                values: new object[] { "12345", "{\"main\": \"em@email.com\",\"emails\": [\"em@email.com\",\"em@yahoo.com\"]}", "Minh", "Nguyen Le", "{\"main\": \"333444\",\"mobiles\": [\"333444\",\"555666\"]}", new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942"), "{\"main\": \"1234\",\"work\": [\"1234\",\"5678\"], \"private\": [\"91011\"]}", "image" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrganizationId",
                table: "Groups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}

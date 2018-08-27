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
                    { new Guid("a316c1e5-5521-487a-8588-d5c8c0014467"), "Rosen" },
                    { new Guid("fcb82b85-8646-4ed4-a410-29ee0c03893a"), "UIT" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6424b41c-136f-4347-ac47-18a672fad8e0"), "Technical Lead" },
                    { new Guid("4b7a6ca9-03c0-4e2c-a856-f05e87a95e31"), "HR Lead" },
                    { new Guid("03ffbf68-6df8-4ea5-a2b3-101e865d34a9"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "OrganizationId" },
                values: new object[,]
                {
                    { new Guid("3e88800d-17b1-4de3-aecc-a4a3960ff059"), "Technical", new Guid("a316c1e5-5521-487a-8588-d5c8c0014467") },
                    { new Guid("3776f864-6f67-4e75-b79b-8dc66ef1c79a"), "HR", new Guid("a316c1e5-5521-487a-8588-d5c8c0014467") },
                    { new Guid("14c5836f-285b-4f10-b3c3-03f35aa84146"), "SoftwareEngineer", new Guid("fcb82b85-8646-4ed4-a410-29ee0c03893a") },
                    { new Guid("f13be28b-8509-408e-841d-03d900d73435"), "ComputerEngineer", new Guid("fcb82b85-8646-4ed4-a410-29ee0c03893a") }
                });

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

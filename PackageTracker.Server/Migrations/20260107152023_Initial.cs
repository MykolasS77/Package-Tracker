using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PackageTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipientInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientInformation_PackageInformations_PackageRef",
                        column: x => x.PackageRef,
                        principalTable: "PackageInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SenderInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenderInformation_PackageInformations_PackageRef",
                        column: x => x.PackageRef,
                        principalTable: "PackageInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PackageRef = table.Column<int>(type: "int", nullable: false),
                    DisplayDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusHistories_PackageInformations_PackageRef",
                        column: x => x.PackageRef,
                        principalTable: "PackageInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PackageInformations",
                column: "Id",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8,
                    9,
                    10
                });

            migrationBuilder.InsertData(
                table: "RecipientInformation",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "PackageRef", "Phone" },
                values: new object[,]
                {
                    { 1, "Otherst010", "Some", "Guy", 1, "123456789" },
                    { 2, "Otherst010", "Some", "Guy", 2, "123456789" },
                    { 3, "Otherst010", "Some", "Guy", 3, "123456789" },
                    { 4, "Otherst010", "Some", "Guy", 4, "123456789" },
                    { 5, "Otherst010", "Some", "Guy", 5, "123456789" },
                    { 6, "Otherst010", "Some", "Guy", 6, "123456789" },
                    { 7, "Otherst010", "Some", "Guy", 7, "123456789" },
                    { 8, "Otherst010", "Some", "Guy", 8, "123456789" },
                    { 9, "Otherst010", "Some", "Guy", 9, "123456789" },
                    { 10, "Otherst010", "Some", "Guy", 10, "123456789" }
                });

            migrationBuilder.InsertData(
                table: "SenderInformation",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "PackageRef", "Phone" },
                values: new object[,]
                {
                    { 1, "Somest101", "John", "Doe", 1, "888888888" },
                    { 2, "Somest101", "John", "Doe", 2, "888888888" },
                    { 3, "Somest101", "John", "Doe", 3, "888888888" },
                    { 4, "Somest101", "John", "Doe", 4, "888888888" },
                    { 5, "Somest101", "John", "Doe", 5, "888888888" },
                    { 6, "Somest101", "John", "Doe", 6, "888888888" },
                    { 7, "Somest101", "John", "Doe", 7, "888888888" },
                    { 8, "Somest101", "John", "Doe", 8, "888888888" },
                    { 9, "Somest101", "John", "Doe", 9, "888888888" },
                    { 10, "Somest101", "John", "Doe", 10, "888888888" }
                });

            migrationBuilder.InsertData(
                table: "StatusHistories",
                columns: new[] { "Id", "DisplayDate", "PackageRef", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 0 },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 0 },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 0 },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 0 },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 0 },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipientInformation_PackageRef",
                table: "RecipientInformation",
                column: "PackageRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SenderInformation_PackageRef",
                table: "SenderInformation",
                column: "PackageRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusHistories_PackageRef",
                table: "StatusHistories",
                column: "PackageRef");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipientInformation");

            migrationBuilder.DropTable(
                name: "SenderInformation");

            migrationBuilder.DropTable(
                name: "StatusHistories");

            migrationBuilder.DropTable(
                name: "PackageInformations");
        }
    }
}

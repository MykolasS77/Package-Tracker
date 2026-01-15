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
                name: "SenderAndRecipientDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderAndRecipientDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenderAndRecipientDetails_PackageInformations_PackageRef",
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
                table: "SenderAndRecipientDetails",
                columns: new[] { "Id", "PackageRef", "RecipientAddress", "RecipientFirstName", "RecipientLastName", "RecipientPhone", "SenderAddress", "SenderFirstName", "SenderLastName", "SenderPhone" },
                values: new object[,]
                {
                    { 1, 1, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 2, 2, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 3, 3, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 4, 4, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 5, 5, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 6, 6, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 7, 7, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 8, 8, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 9, 9, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" },
                    { 10, 10, "Otherst010", "Some", "Guy", "123456789", "Somest101", "John", "Doe", "888888888" }
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
                name: "IX_SenderAndRecipientDetails_PackageRef",
                table: "SenderAndRecipientDetails",
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
                name: "SenderAndRecipientDetails");

            migrationBuilder.DropTable(
                name: "StatusHistories");

            migrationBuilder.DropTable(
                name: "PackageInformations");
        }
    }
}

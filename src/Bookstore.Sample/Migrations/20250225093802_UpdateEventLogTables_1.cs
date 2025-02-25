using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Sample.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventLogTables_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PropertyTypeDescriptions",
                newName: "PropertyTypeDescriptions",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "EntityTypeDescriptions",
                newName: "EntityTypeDescriptions",
                newSchema: "eventlog");

            migrationBuilder.CreateTable(
                name: "DateTimePropertyLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EntityLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimePropertyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateTimePropertyLog_EntityLog_EntityLogEntryId",
                        column: x => x.EntityLogEntryId,
                        principalTable: "EntityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoublePropertyLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    EntityLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoublePropertyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoublePropertyLog_EntityLog_EntityLogEntryId",
                        column: x => x.EntityLogEntryId,
                        principalTable: "EntityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateTimePropertyLog_EntityLogEntryId",
                table: "DateTimePropertyLog",
                column: "EntityLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_DoublePropertyLog_EntityLogEntryId",
                table: "DoublePropertyLog",
                column: "EntityLogEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateTimePropertyLog");

            migrationBuilder.DropTable(
                name: "DoublePropertyLog");

            migrationBuilder.RenameTable(
                name: "PropertyTypeDescriptions",
                schema: "eventlog",
                newName: "PropertyTypeDescriptions");

            migrationBuilder.RenameTable(
                name: "EntityTypeDescriptions",
                schema: "eventlog",
                newName: "EntityTypeDescriptions");
        }
    }
}

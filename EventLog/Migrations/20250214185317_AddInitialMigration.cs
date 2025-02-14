using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventLog.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "eventlog");

            migrationBuilder.CreateTable(
                name: "EventLog",
                schema: "eventlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventType = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true),
                    FailureDetails = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStatusDescriptions",
                schema: "eventlog",
                columns: table => new
                {
                    EnumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStatusDescriptions", x => x.EnumId);
                });

            migrationBuilder.CreateTable(
                name: "EventTypeDescriptions",
                schema: "eventlog",
                columns: table => new
                {
                    EnumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypeDescriptions", x => x.EnumId);
                });

            migrationBuilder.CreateTable(
                name: "EntityLog",
                schema: "eventlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActionType = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityType = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityLog_EventLog_EventLogEntryId",
                        column: x => x.EventLogEntryId,
                        principalSchema: "eventlog",
                        principalTable: "EventLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoolPropertyLog",
                schema: "eventlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<bool>(type: "INTEGER", nullable: false),
                    EntityLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoolPropertyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoolPropertyLog_EntityLog_EntityLogEntryId",
                        column: x => x.EntityLogEntryId,
                        principalSchema: "eventlog",
                        principalTable: "EntityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DecimalPropertyLog",
                schema: "eventlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    EntityLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalPropertyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalPropertyLog_EntityLog_EntityLogEntryId",
                        column: x => x.EntityLogEntryId,
                        principalSchema: "eventlog",
                        principalTable: "EntityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Int32PropertyLog",
                schema: "eventlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Int32PropertyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Int32PropertyLog_EntityLog_EntityLogEntryId",
                        column: x => x.EntityLogEntryId,
                        principalSchema: "eventlog",
                        principalTable: "EntityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StringPropertyLog",
                schema: "eventlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropertyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    EntityLogEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringPropertyLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringPropertyLog_EntityLog_EntityLogEntryId",
                        column: x => x.EntityLogEntryId,
                        principalSchema: "eventlog",
                        principalTable: "EntityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoolPropertyLog_EntityLogEntryId",
                schema: "eventlog",
                table: "BoolPropertyLog",
                column: "EntityLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalPropertyLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DecimalPropertyLog",
                column: "EntityLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLog_EventLogEntryId",
                schema: "eventlog",
                table: "EntityLog",
                column: "EventLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Int32PropertyLog_EntityLogEntryId",
                schema: "eventlog",
                table: "Int32PropertyLog",
                column: "EntityLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_StringPropertyLog_EntityLogEntryId",
                schema: "eventlog",
                table: "StringPropertyLog",
                column: "EntityLogEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoolPropertyLog",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "DecimalPropertyLog",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "EventStatusDescriptions",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "EventTypeDescriptions",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "Int32PropertyLog",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "StringPropertyLog",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "EntityLog",
                schema: "eventlog");

            migrationBuilder.DropTable(
                name: "EventLog",
                schema: "eventlog");
        }
    }
}

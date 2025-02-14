using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventLog.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddApplicationDbContextInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestString = table.Column<string>(type: "TEXT", nullable: true),
                    TestBool = table.Column<bool>(type: "INTEGER", nullable: false),
                    TestInt32 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestData");
        }
    }
}

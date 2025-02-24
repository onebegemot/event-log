using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Sample.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventLogTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityTypeDescriptions",
                columns: table => new
                {
                    EnumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypeDescriptions", x => x.EnumId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypeDescriptions",
                columns: table => new
                {
                    EnumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypeDescriptions", x => x.EnumId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityTypeDescriptions");

            migrationBuilder.DropTable(
                name: "PropertyTypeDescriptions");
        }
    }
}

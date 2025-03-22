using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Sample.Migrations
{
    /// <inheritdoc />
    public partial class OptimizeTablePropertyTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationEntities_ApplicationOtherEntities_ShelfId",
                table: "ApplicationEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_BoolPropertyLog_EntityLog_EntityLogEntryId",
                table: "BoolPropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_DateTimePropertyLog_EntityLog_EntityLogEntryId",
                table: "DateTimePropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_DecimalPropertyLog_EntityLog_EntityLogEntryId",
                table: "DecimalPropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_DoublePropertyLog_EntityLog_EntityLogEntryId",
                table: "DoublePropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityLog_EventLog_EventLogEntryId",
                table: "EntityLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Int32PropertyLog_EntityLog_EntityLogEntryId",
                table: "Int32PropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_StringPropertyLog_EntityLog_EntityLogEntryId",
                table: "StringPropertyLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationOtherEntities",
                table: "ApplicationOtherEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationEntities",
                table: "ApplicationEntities");

            migrationBuilder.RenameTable(
                name: "StringPropertyLog",
                newName: "StringPropertyLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "Int32PropertyLog",
                newName: "Int32PropertyLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "EventLog",
                newName: "EventLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "EntityLog",
                newName: "EntityLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "DoublePropertyLog",
                newName: "DoublePropertyLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "DecimalPropertyLog",
                newName: "DecimalPropertyLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "DateTimePropertyLog",
                newName: "DateTimePropertyLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "BoolPropertyLog",
                newName: "BoolPropertyLog",
                newSchema: "eventlog");

            migrationBuilder.RenameTable(
                name: "ApplicationOtherEntities",
                newName: "Shelves");

            migrationBuilder.RenameTable(
                name: "ApplicationEntities",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationEntities_ShelfId",
                table: "Books",
                newName: "IX_Books_ShelfId");

            migrationBuilder.AlterColumn<short>(
                name: "EnumId",
                schema: "eventlog",
                table: "PropertyTypeDescriptions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "EnumId",
                schema: "eventlog",
                table: "EventTypeDescriptions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<byte>(
                name: "EnumId",
                schema: "eventlog",
                table: "EventStatusDescriptions",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "EnumId",
                schema: "eventlog",
                table: "EntityTypeDescriptions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "PropertyType",
                schema: "eventlog",
                table: "StringPropertyLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "PropertyType",
                schema: "eventlog",
                table: "Int32PropertyLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "eventlog",
                table: "EventLog",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "EventType",
                schema: "eventlog",
                table: "EventLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "EntityType",
                schema: "eventlog",
                table: "EntityLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<byte>(
                name: "ActionType",
                schema: "eventlog",
                table: "EntityLog",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "PropertyType",
                schema: "eventlog",
                table: "DoublePropertyLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "PropertyType",
                schema: "eventlog",
                table: "DecimalPropertyLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "PropertyType",
                schema: "eventlog",
                table: "DateTimePropertyLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<short>(
                name: "PropertyType",
                schema: "eventlog",
                table: "BoolPropertyLog",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StringPropertyLog_PropertyType",
                schema: "eventlog",
                table: "StringPropertyLog",
                column: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_Int32PropertyLog_PropertyType",
                schema: "eventlog",
                table: "Int32PropertyLog",
                column: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_CreatedAt",
                schema: "eventlog",
                table: "EventLog",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLog_EntityId",
                schema: "eventlog",
                table: "EntityLog",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DoublePropertyLog_PropertyType",
                schema: "eventlog",
                table: "DoublePropertyLog",
                column: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalPropertyLog_PropertyType",
                schema: "eventlog",
                table: "DecimalPropertyLog",
                column: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimePropertyLog_PropertyType",
                schema: "eventlog",
                table: "DateTimePropertyLog",
                column: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_BoolPropertyLog_PropertyType",
                schema: "eventlog",
                table: "BoolPropertyLog",
                column: "PropertyType");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shelves_ShelfId",
                table: "Books",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolPropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "BoolPropertyLog",
                column: "EntityLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DateTimePropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DateTimePropertyLog",
                column: "EntityLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DecimalPropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DecimalPropertyLog",
                column: "EntityLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoublePropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DoublePropertyLog",
                column: "EntityLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityLog_EventLog_EventLogEntryId",
                schema: "eventlog",
                table: "EntityLog",
                column: "EventLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EventLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Int32PropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "Int32PropertyLog",
                column: "EntityLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StringPropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "StringPropertyLog",
                column: "EntityLogEntryId",
                principalSchema: "eventlog",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shelves_ShelfId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BoolPropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "BoolPropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_DateTimePropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DateTimePropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_DecimalPropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DecimalPropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_DoublePropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "DoublePropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityLog_EventLog_EventLogEntryId",
                schema: "eventlog",
                table: "EntityLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Int32PropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "Int32PropertyLog");

            migrationBuilder.DropForeignKey(
                name: "FK_StringPropertyLog_EntityLog_EntityLogEntryId",
                schema: "eventlog",
                table: "StringPropertyLog");

            migrationBuilder.DropIndex(
                name: "IX_StringPropertyLog_PropertyType",
                schema: "eventlog",
                table: "StringPropertyLog");

            migrationBuilder.DropIndex(
                name: "IX_Int32PropertyLog_PropertyType",
                schema: "eventlog",
                table: "Int32PropertyLog");

            migrationBuilder.DropIndex(
                name: "IX_EventLog_CreatedAt",
                schema: "eventlog",
                table: "EventLog");

            migrationBuilder.DropIndex(
                name: "IX_EntityLog_EntityId",
                schema: "eventlog",
                table: "EntityLog");

            migrationBuilder.DropIndex(
                name: "IX_DoublePropertyLog_PropertyType",
                schema: "eventlog",
                table: "DoublePropertyLog");

            migrationBuilder.DropIndex(
                name: "IX_DecimalPropertyLog_PropertyType",
                schema: "eventlog",
                table: "DecimalPropertyLog");

            migrationBuilder.DropIndex(
                name: "IX_DateTimePropertyLog_PropertyType",
                schema: "eventlog",
                table: "DateTimePropertyLog");

            migrationBuilder.DropIndex(
                name: "IX_BoolPropertyLog_PropertyType",
                schema: "eventlog",
                table: "BoolPropertyLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "StringPropertyLog",
                schema: "eventlog",
                newName: "StringPropertyLog");

            migrationBuilder.RenameTable(
                name: "Int32PropertyLog",
                schema: "eventlog",
                newName: "Int32PropertyLog");

            migrationBuilder.RenameTable(
                name: "EventLog",
                schema: "eventlog",
                newName: "EventLog");

            migrationBuilder.RenameTable(
                name: "EntityLog",
                schema: "eventlog",
                newName: "EntityLog");

            migrationBuilder.RenameTable(
                name: "DoublePropertyLog",
                schema: "eventlog",
                newName: "DoublePropertyLog");

            migrationBuilder.RenameTable(
                name: "DecimalPropertyLog",
                schema: "eventlog",
                newName: "DecimalPropertyLog");

            migrationBuilder.RenameTable(
                name: "DateTimePropertyLog",
                schema: "eventlog",
                newName: "DateTimePropertyLog");

            migrationBuilder.RenameTable(
                name: "BoolPropertyLog",
                schema: "eventlog",
                newName: "BoolPropertyLog");

            migrationBuilder.RenameTable(
                name: "Shelves",
                newName: "ApplicationOtherEntities");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "ApplicationEntities");

            migrationBuilder.RenameIndex(
                name: "IX_Books_ShelfId",
                table: "ApplicationEntities",
                newName: "IX_ApplicationEntities_ShelfId");

            migrationBuilder.AlterColumn<int>(
                name: "EnumId",
                schema: "eventlog",
                table: "PropertyTypeDescriptions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "EnumId",
                schema: "eventlog",
                table: "EventTypeDescriptions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "EnumId",
                schema: "eventlog",
                table: "EventStatusDescriptions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "EnumId",
                schema: "eventlog",
                table: "EntityTypeDescriptions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyType",
                table: "StringPropertyLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyType",
                table: "Int32PropertyLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EventLog",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "EventType",
                table: "EventLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "EntityType",
                table: "EntityLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "ActionType",
                table: "EntityLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyType",
                table: "DoublePropertyLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyType",
                table: "DecimalPropertyLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyType",
                table: "DateTimePropertyLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyType",
                table: "BoolPropertyLog",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationOtherEntities",
                table: "ApplicationOtherEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationEntities",
                table: "ApplicationEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationEntities_ApplicationOtherEntities_ShelfId",
                table: "ApplicationEntities",
                column: "ShelfId",
                principalTable: "ApplicationOtherEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolPropertyLog_EntityLog_EntityLogEntryId",
                table: "BoolPropertyLog",
                column: "EntityLogEntryId",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DateTimePropertyLog_EntityLog_EntityLogEntryId",
                table: "DateTimePropertyLog",
                column: "EntityLogEntryId",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DecimalPropertyLog_EntityLog_EntityLogEntryId",
                table: "DecimalPropertyLog",
                column: "EntityLogEntryId",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoublePropertyLog_EntityLog_EntityLogEntryId",
                table: "DoublePropertyLog",
                column: "EntityLogEntryId",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityLog_EventLog_EventLogEntryId",
                table: "EntityLog",
                column: "EventLogEntryId",
                principalTable: "EventLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Int32PropertyLog_EntityLog_EntityLogEntryId",
                table: "Int32PropertyLog",
                column: "EntityLogEntryId",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StringPropertyLog_EntityLog_EntityLogEntryId",
                table: "StringPropertyLog",
                column: "EntityLogEntryId",
                principalTable: "EntityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

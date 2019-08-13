using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSite.Migrations.BillQuery
{
    public partial class BillQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fixed = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    Sortable = table.Column<bool>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConnectionString = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicText",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: false),
                    ColumnInfoId = table.Column<int>(nullable: true),
                    FieldMenuItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicText_ColumnInfo_ColumnInfoId",
                        column: x => x.ColumnInfoId,
                        principalTable: "ColumnInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Method",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ConnectionId = table.Column<int>(nullable: true),
                    DynamicTextId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Method", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Method_Connection_ConnectionId",
                        column: x => x.ConnectionId,
                        principalTable: "Connection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Method_DynamicText_DynamicTextId",
                        column: x => x.DynamicTextId,
                        principalTable: "DynamicText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MethodId = table.Column<int>(nullable: false),
                    Params = table.Column<string>(nullable: false),
                    ColumnInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldMenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldMenuItem_ColumnInfo_ColumnInfoId",
                        column: x => x.ColumnInfoId,
                        principalTable: "ColumnInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldMenuItem_Method_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Method",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicText_ColumnInfoId",
                table: "DynamicText",
                column: "ColumnInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicText_FieldMenuItemId",
                table: "DynamicText",
                column: "FieldMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldMenuItem_ColumnInfoId",
                table: "FieldMenuItem",
                column: "ColumnInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldMenuItem_MethodId",
                table: "FieldMenuItem",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Method_ConnectionId",
                table: "Method",
                column: "ConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Method_DynamicTextId",
                table: "Method",
                column: "DynamicTextId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicText_FieldMenuItem_FieldMenuItemId",
                table: "DynamicText",
                column: "FieldMenuItemId",
                principalTable: "FieldMenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicText_ColumnInfo_ColumnInfoId",
                table: "DynamicText");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldMenuItem_ColumnInfo_ColumnInfoId",
                table: "FieldMenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicText_FieldMenuItem_FieldMenuItemId",
                table: "DynamicText");

            migrationBuilder.DropTable(
                name: "ColumnInfo");

            migrationBuilder.DropTable(
                name: "FieldMenuItem");

            migrationBuilder.DropTable(
                name: "Method");

            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "DynamicText");
        }
    }
}

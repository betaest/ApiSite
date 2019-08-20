using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSite.Migrations.BillQuery
{
    public partial class BillQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "column",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Sortable = table.Column<bool>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_column", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "value",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: false),
                    ConnectionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_value_Connection_ConnectionName",
                        column: x => x.ConnectionName,
                        principalTable: "Connection",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "result",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    ValueId = table.Column<int>(nullable: false),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_result_result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_result_value_ValueId",
                        column: x => x.ValueId,
                        principalTable: "value",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menu_item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    ColumnKey = table.Column<string>(nullable: false),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menu_item_column_ColumnKey",
                        column: x => x.ColumnKey,
                        principalTable: "column",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_menu_item_result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_ColumnKey",
                table: "menu_item",
                column: "ColumnKey");

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_ResultId",
                table: "menu_item",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_result_ResultId",
                table: "result",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_result_ValueId",
                table: "result",
                column: "ValueId");

            migrationBuilder.CreateIndex(
                name: "IX_value_ConnectionName",
                table: "value",
                column: "ConnectionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_item");

            migrationBuilder.DropTable(
                name: "column");

            migrationBuilder.DropTable(
                name: "result");

            migrationBuilder.DropTable(
                name: "value");

            migrationBuilder.DropTable(
                name: "Connection");
        }
    }
}

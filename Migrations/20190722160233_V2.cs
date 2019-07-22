using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSite.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogonHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    StaffName = table.Column<string>(nullable: false),
                    IpAddr = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogonHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Department = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Handler = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OperateDateTime = table.Column<DateTime>(nullable: false),
                    Operator = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    ProjectInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectAttachment_ProjectInfo_ProjectInfoId",
                        column: x => x.ProjectInfoId,
                        principalTable: "ProjectInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAttachment_ProjectInfoId",
                table: "ProjectAttachment",
                column: "ProjectInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogonHistory");

            migrationBuilder.DropTable(
                name: "ProjectAttachment");

            migrationBuilder.DropTable(
                name: "ProjectInfo");
        }
    }
}
